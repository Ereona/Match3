using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGemActionImplementation : GameActionImplementation
{
    protected override IEnumerator PerformImpl()
    {
        MoveGemGameAction moveAction = SourceAction as MoveGemGameAction;
        CellObject cellFrom = Objects.Cells.Find(moveAction.From);
        CellObject cellTo = Objects.Cells.Find(moveAction.To);
        GemObject g1 = Objects.Gems.Find(moveAction.Gem);
        yield return Move(cellFrom, cellTo, g1);
    }
}
