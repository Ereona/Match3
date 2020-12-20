using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGemGenerationSettings : GemGenerationSettings
{
    public RandomGemGenerationSettings(int[] colors, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int colorIndex = Random.Range(0, colors.Length);
            Add(colors[colorIndex], 1);
        }
    }
}
