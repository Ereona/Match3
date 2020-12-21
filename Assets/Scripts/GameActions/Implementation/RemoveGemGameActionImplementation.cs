using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGemGameActionImplementation : GameActionImplementation
{
    protected override IEnumerator PerformImpl()
    {
        RemoveGemGameAction source = SourceAction as RemoveGemGameAction;
        GemObject g = Objects.Gems.Find(source.Gem);
        yield return Scale(g, Vector3.one, Vector3.zero);
        SimplePool.Despawn(g.gameObject);
    }
}
