using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public CellEventChannelSO CellClickEvent;
    public CellEventChannelSO SelectionChangedEvent;

    private Field FieldWithGems;
    private Cell SelectedCell;
    private MatchesCounter Counter;
    private GemsFallCounter FallCounter;
    private GameActionsContainer ActionsContainer;

    public void Init(Field field)
    {
        FieldWithGems = field;
        Counter = new MatchesCounter(field);
        FallCounter = new GemsFallCounter(field);
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
                    if (movingActions.Count > 0)
                    {
                        ActionsContainer.Clear();
                        foreach (GameAction a in movingActions)
                        {
                            ActionsContainer.Add(a);
                        }
                        yield return PerformGameActions();
                    }
                    else
                    {
                        movingFinished = true;
                    }
                }
            }
            else
            {
                finished = true;
            }
        }
    }

    private IEnumerator PerformGameActions()
    {
        State = GameState.Animation;
        yield return ActionsContainer.PerformAll();
        State = GameState.Selection;
    }
}
