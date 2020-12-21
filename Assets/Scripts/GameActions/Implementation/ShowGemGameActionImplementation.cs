using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGemGameActionImplementation : GameActionImplementation
{
    protected override IEnumerator PerformImpl()
    {
        ShowGemGameAction source = SourceAction as ShowGemGameAction;
        FieldBuilder builder = GameObject.FindObjectOfType<FieldBuilder>();
        GemObject gemObject = builder.BuildGemInCell(source.Cell);
        yield return Scale(gemObject, new Vector3(0, 1, 1), Vector3.one);
    }
}
