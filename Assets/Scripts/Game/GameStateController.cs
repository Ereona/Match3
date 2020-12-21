using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public CellEventChannelSO CellClickEvent;
    public CellEventChannelSO SelectionChangedEvent;

    private Field FieldWithGems;
    private Cell SelectedCell;
    private MatchesCounter Counter;
    private GemsFallCounter FallCounter;
    private GemsSpawnCounter SpawnCounter;
    private PossibleMovesDetector MovesDetector;
    private GameActionsContainer ActionsContainer;
    private GemsGenerator Generator;

    public void Init(Field field, GemsGenerator generator, List<int> colors)
    {
        FieldWithGems = field;
        Generator = generator;
        Counter = new MatchesCounter(field);
        FallCounter = new GemsFallCounter(field);
        SpawnCounter = new GemsSpawnCounter(field, colors);
        MovesDetector = new PossibleMovesDetector(field);
        ActionsContainer = GetComponent<GameActionsContainer>();
    }

    public GameState State
    {
        get; private set;
    }

    void OnEnable()
    {
        CellClickEvent.OnEventRaised += OnCellClick;
    }

    private void OnDisable()
    {
        CellClickEvent.OnEventRaised -= OnCellClick;
    }

    private void OnCellClick(Cell clicked)
    {
        if (State != GameState.Selection)
        {
            return;
        }
        if (GameRules.IsCellSelectable(clicked.type))
        {
            if (SelectedCell != null && Counter.AreNeighbors(SelectedCell, clicked))
            {
                StartCoroutine(AfterSecondCellClicked(clicked));
            }
            else
            {
                SelectedCell = clicked;
                State = GameState.Selection;
                SelectionChangedEvent.RaiseEvent(clicked);
            }
        }
    }

    private IEnumerator AfterSecondCellClicked(Cell clicked)
    {
        ActionsContainer.Clear();
        TwoCellsGameAction movingAction;
        FieldWithGems.Swap(SelectedCell, clicked);
        if (Counter.HasMatchesAfterSwap(SelectedCell, clicked))
        {
            movingAction = new GemsSwapGameAction();
        }
        else
        {
            movingAction = new GemsBothWayMovingGameAction();
            FieldWithGems.Swap(SelectedCell, clicked);
        }
        movingAction.Gem1 = SelectedCell.GemInCell;
        movingAction.Gem2 = clicked.GemInCell;
        ActionsContainer.Add(movingAction);

        SelectedCell = null;
        SelectionChangedEvent.RaiseEvent(null);

        yield return StartCoroutine(PerformGameActions());
        yield return UpdateField();
    }

    private IEnumerator UpdateField()
    {
        bool finished = false;
        while (!finished)
        {
            MatchesList matches = Counter.FindAllMatches();
            if (matches.GetAllCells().Count > 0)
            {
                yield return ApplyMatchesAndMove(matches);
            }
            else
            {
                finished = true;
            }
        }
        if (!MovesDetector.MoveExists())
        {
            yield return MixGems();
        }
    }

    private IEnumerator ApplyMatchesAndMove(MatchesList matches)
    {
        ActionsContainer.Clear();
        foreach (Cell c in matches.GetAllCells())
        {
            RemoveGemGameAction action = new RemoveGemGameAction();
            action.RemovingGem = c.GemInCell;
            ActionsContainer.Add(action);
        }
        FieldWithGems.ClearCells(matches.GetAllCells());
        yield return PerformGameActions();
        bool movingFinished = false;
        while (!movingFinished)
        {
            List<GameAction> movingActions = FallCounter.DoMoving();
            movingActions.AddRange(SpawnCounter.CalcSpawnActions());
            if (movingActions.Count > 0)
            {
                ActionsContainer.Clear();
                ActionsContainer.AddRange(movingActions);
                yield return PerformGameActions();
            }
            else
            {
                movingFinished = true;
            }
        }
    }

    private IEnumerator MixGems()
    {
        List<Cell> cells = FieldWithGems.GetFilledCells();
        ActionsContainer.Clear();
        Dictionary<int, int> counts = new Dictionary<int, int>();
        foreach (Cell c in cells)
        {
            HideGemGameAction hideAction = new HideGemGameAction();
            hideAction.Gem = c.GemInCell;
            ActionsContainer.Add(hideAction);
            if (counts.ContainsKey(c.GemInCell.colorId))
            {
                counts[c.GemInCell.colorId]++;
            }
            else
            {
                counts[c.GemInCell.colorId] = 1;
            }
        }
        FieldWithGems.Clear();
        yield return PerformGameActions();
        ConstGemGenerationSettings genSettings = new ConstGemGenerationSettings(counts);
        Generator.Generate(cells, genSettings);
        ActionsContainer.Clear();
        foreach (Cell c in cells)
        {
            ShowGemGameAction showAction = new ShowGemGameAction();
            showAction.Cell = c;
            ActionsContainer.Add(showAction);
        }
        yield return PerformGameActions();
    }

    private IEnumerator PerformGameActions()
    {
        State = GameState.Animation;
        yield return ActionsContainer.PerformAll();
        State = GameState.Selection;
    }
}
