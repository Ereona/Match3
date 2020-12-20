using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMovingGameActionImplementation : GameActionImplementation
{
    public Cell From;
    public Cell To;

    public override IEnumerator Perform()
    {
        IsPerformed = false;
        CellObject cellFrom = Objects.Cells.Find(From);
        CellObject cellTo = Objects.Cells.Find(To);
        Gem gem = From.GemInCell;
        GemObject g1 = Objects.Gems.Find(gem);
        yield return Move(cellFrom, cellTo, g1);
        IsPerformed = true;
    }
}
