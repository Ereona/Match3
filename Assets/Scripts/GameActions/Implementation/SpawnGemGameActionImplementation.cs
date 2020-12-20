using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGemGameActionImplementation : GameActionImplementation
{
    protected override IEnumerator PerformImpl()
    {
        SpawnGemGameAction source = SourceAction as SpawnGemGameAction;
        FieldBuilder builder = GameObject.FindObjectOfType<FieldBuilder>();
        GemObject gemObject = builder.BuildGemInCell(source.Cell);
        yield return Scale(gemObject, Vector3.zero, Vector3.one);
    }
}
