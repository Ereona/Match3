using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsBothWayMovingGameActionImplementation : GameActionImplementation
{
    protected override IEnumerator PerformImpl()
    {
        TwoCellsGameAction source = SourceAction as TwoCellsGameAction;
        GemObject g1 = Objects.Gems.Find(source.Gem1);
        GemObject g2 = Objects.Gems.Find(source.Gem2);
        yield return Swap(g1, g2);
        yield return Swap(g1, g2);
    }
}
