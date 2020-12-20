using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGemGameActionImplementation : GameActionImplementation
{
    protected override IEnumerator PerformImpl()
    {
        RemoveGemGameAction source = SourceAction as RemoveGemGameAction;
        GemObject g = Objects.Gems.Find(source.RemovingCell.GemInCell);
        float t = 0;
        while (t < Duration)
        {
            g.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t / Duration);
            t += Time.deltaTime;
            yield return null;
        }
        g.transform.localScale = Vector3.zero;
        Object.Destroy(g.gameObject);
    }
}
