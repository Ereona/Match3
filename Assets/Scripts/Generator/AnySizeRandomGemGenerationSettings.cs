using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnySizeRandomGemGenerationSettings : GemGenerationSettings
{
    private int[] colors;

    public AnySizeRandomGemGenerationSettings(int[] colors)
    {
        this.colors = colors;
    }

    public override List<int> GetAllColors()
    {
        return colors.ToList();
    }

    public override int GetPreferredColor(List<int> possibleColors)
    {
        int index = Random.Range(0, possibleColors.Count);
        return possibleColors[index];
    }

    public override void SubstractOne(int colorId)
    {
    }
}
