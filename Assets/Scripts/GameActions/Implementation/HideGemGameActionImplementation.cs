using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGemGameActionImplementation : GameActionImplementation
{
    protected override IEnumerator PerformImpl()
    {
        HideGemGameAction source = SourceAction as HideGemGameAction;
        GemObject gemObject = Objects.Gems.Find(source.Gem);
        yield return Scale(gemObject, Vector3.one, new Vector3(0, 1, 1));
        SimplePool.Despawn(gemObject.gameObject);
    }
}
