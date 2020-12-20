using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsBothWayMovingGameActionImplementation : GameActionImplementation
{
    public override IEnumerator Perform()
    {
        IsPerformed = false;
        TwoCellsGameAction source = SourceAction as TwoCellsGameAction;
        CellObject c1 = Objects.Cells.Find(source.Cell1);
        CellObject c2 = Objects.Cells.Find(source.Cell2);
        Gem gem1 = source.Cell1.GemInCell;
        Gem gem2 = source.Cell2.GemInCell;
        GemObject g1 = Objects.Gems.Find(gem1);
        GemObject g2 = Objects.Gems.Find(gem2);
        yield return Swap(c1, c2, g1, g2);
        yield return Swap(c2, c1, g1, g2);
        IsPerformed = true;
    }
}
