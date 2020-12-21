using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstGemGenerationSettings : GemGenerationSettings
{
    public ConstGemGenerationSettings(int[] colors, int[] counts)
    {
        if (counts.Length != colors.Length)
        {
            throw new System.ArgumentException();
        }
        for (int i = 0; i < colors.Length; i++)
        {
            Add(colors[i], counts[i]);
        }
    }

    public ConstGemGenerationSettings(Dictionary<int, int> counts)
    {
        foreach (KeyValuePair<int, int> pair in counts)
        {
            Add(pair.Key, pair.Value);
        }
    }
}
