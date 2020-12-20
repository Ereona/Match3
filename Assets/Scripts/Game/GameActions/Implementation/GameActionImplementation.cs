using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActionImplementation
{
    public bool IsPerformed { get; protected set; }
    public abstract IEnumerator Perform();

    public FieldObjectsContainer Objects;

    public GameAction SourceAction;

    protected IEnumerator Move(CellObject c1, CellObject c2, GemObject g)
    {
        float interval = 0.25f;
        Vector3 c1pos = c1.transform.position;
        Vector3 c2pos = c2.transform.position;
        float t = 0;
        while (t < interval)
        {
            g.transform.position = Vector3.Lerp(c1pos, c2pos, t / interval);
            t += Time.deltaTime;
            yield return null;
        }
        g.transform.position = c2pos;
    }

    protected IEnumerator Swap(CellObject c1, CellObject c2, GemObject g1, GemObject g2)
    {
        float interval = 0.25f;
        Vector3 c1pos = c1.transform.position;
        Vector3 c2pos = c2.transform.position;
        float t = 0;
        while (t < interval)
        {
            g1.transform.position = Vector3.Lerp(c1pos, c2pos, t / interval);
            g2.transform.position = Vector3.Lerp(c2pos, c1pos, t / interval);
            t += Time.deltaTime;
            yield return null;
        }
        g1.transform.position = c2pos;
        g2.transform.position = c1pos;
    }
}
