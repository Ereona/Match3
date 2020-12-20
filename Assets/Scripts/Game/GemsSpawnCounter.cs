﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemsSpawnCounter : MonoBehaviour
{
    private Field FieldWithGems;
    GemGenerationSettings genSettings;

    public GemsSpawnCounter(Field field, int[] colors)
    {
        FieldWithGems = field;
        genSettings = new AnySizeRandomGemGenerationSettings(colors);
    }

    public List<GameAction> CalcSpawnActions()
    {
        List<GameAction> result = new List<GameAction>();
        for (int i = 0; i < FieldWithGems.Rows; i++)
        {
            for (int j = 0; j < FieldWithGems.Cols; j++)
            {
                Cell c = FieldWithGems[i, j];
                if (c != null && c.type == CellType.Spawner && c.GemInCell == null)
                {
                    SpawnGemGameAction spawn = new SpawnGemGameAction();
                    spawn.Cell = c;
                    Gem gem = new Gem();
                    gem.colorId = genSettings.GetPreferredColor(new int[] { 0, 1, 2, 3 }.ToList());
                    c.GemInCell = gem;
                    result.Add(spawn);
                }
            }
        }
        return result;
    }
}
