using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleMovePattern
{
    public int PatternType { get; private set; }
    public int Rotation { get; private set; }
    public bool Mirrored { get; private set; }

    public PossibleMovePattern(int type, int rotation, bool mirrored)
    {
        PatternType = type;
        Rotation = rotation;
        Mirrored = mirrored;
    }

    public List<Vector2Int> GetPattern()
    {
        List<Vector2Int> pattern = new List<Vector2Int>();
        if (PatternType == 1)
        {
            pattern.Add(new Vector2Int(0, 1));
            if (Mirrored)
            {
                pattern.Add(new Vector2Int(-1, 2));
            }
            else
            {
                pattern.Add(new Vector2Int(1, 2));
            }
        }
        else if (PatternType == 2)
        {
            if (Mirrored)
            {
                pattern.Add(new Vector2Int(-1, 1));
            }
            else
            {
                pattern.Add(new Vector2Int(1, 1));
            }
            pattern.Add(new Vector2Int(0, 2));
        }
        else
        {
            throw new System.NotImplementedException("Unknown pattern type");
        }
        for (int i = 0; i < Rotation; i++)
        {
            for (int j = 0; j < pattern.Count; j++)
            {
                pattern[j] = new Vector2Int(-pattern[j].y, pattern[j].x);
            }
        }
        return pattern;
    }
}
