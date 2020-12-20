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
    private GameActionsContainer ActionsContainer;

    public void Init(Field field)
    {
        FieldWithGems = field;
        Counter = new MatchesCounter(field);
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
        if (Counter.HasMatchesAfterSwap(SelectedCell, clicked))
        {
            movingAction = new GemsSwapGameAction();
        }
        else
        {
            movingAction = new GemsBothWayMovingGameAction();
        }
        movingAction.Cell1 = SelectedCell;
        movingAction.Cell2 = clicked;
        ActionsContainer.Add(movingAction);

        SelectedCell = null;
        SelectionChangedEvent.RaiseEvent(null);

        yield return StartCoroutine(PerformGameActions());
        yield return UpdateField();
    }

    private IEnumerator UpdateField()
    {
        MatchesList matches = Counter.FindAllMatches();
        if (matches.GetAllCells().Count > 0)
        {
            ActionsContainer.Clear();
            foreach (Cell c in matches.GetAllCells())
            {
                RemoveGemGameAction action = new RemoveGemGameAction();
                action.RemovingCell = c;
                ActionsContainer.Add(action);
            }
            yield return PerformGameActions();
        }
    }

    private IEnumerator PerformGameActions()
    {
        State = GameState.Animation;
        yield return ActionsContainer.PerformAll();
        ActionsContainer.OnActionsPerformed(FieldWithGems);
        State = GameState.Selection;
    }
}
