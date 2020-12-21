using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnySizeRandomGemGenerationSettings : GemGenerationSettings
{
    private List<int> colors;

    public AnySizeRandomGemGenerationSettings(List<int> colors)
    {
        this.colors = colors;
    }

    public override List<int> GetAllColors()
    {
        return colors;
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
