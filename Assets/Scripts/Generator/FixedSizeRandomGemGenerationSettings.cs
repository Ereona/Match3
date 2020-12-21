using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSizeRandomGemGenerationSettings : GemGenerationSettings
{
    public FixedSizeRandomGemGenerationSettings(List<int> colors, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int colorIndex = Random.Range(0, colors.Count);
            Add(colors[colorIndex], 1);
        }
    }
}
