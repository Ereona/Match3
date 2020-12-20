using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActionImplementation
{
    public bool IsPerformed { get; private set; }

    public float Duration { get; set; }

    public IEnumerator Perform()
    {
        IsPerformed = false;
        yield return PerformImpl();
        IsPerformed = true;
    }

    protected abstract IEnumerator PerformImpl();

    public FieldObjectsContainer Objects;

    public GameAction SourceAction;

    protected IEnumerator Move(CellObject c1, CellObject c2, GemObject g)
    {
        Vector3 c1pos = c1.transform.position;
        Vector3 c2pos = c2.transform.position;
        float t = 0;
        while (t < Duration)
        {
            g.transform.position = Vector3.Lerp(c1pos, c2pos, t / Duration);
            t += Time.deltaTime;
            yield return null;
        }
        g.transform.position = c2pos;
    }

    protected IEnumerator Swap(GemObject g1, GemObject g2)
    {
        Vector3 g1pos = g1.transform.position;
        Vector3 g2pos = g2.transform.position;
        float t = 0;
        while (t < Duration)
        {
            g1.transform.position = Vector3.Lerp(g1pos, g2pos, t / Duration);
            g2.transform.position = Vector3.Lerp(g2pos, g1pos, t / Duration);
            t += Time.deltaTime;
            yield return null;
        }
        g1.transform.position = g2pos;
        g2.transform.position = g1pos;
    }
}
