using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GemGenerationSettings
{
    private List<GenerationSettingsItem> infos = new List<GenerationSettingsItem>();

    public virtual List<int> GetAllColors()
    {
        return infos.Select(c => c.gemColorId).ToList();
    }

    public virtual int GetPreferredColor(List<int> possibleColors)
    {
        List<GenerationSettingsItem> genInfosOrdered = infos.Where(c => possibleColors.Contains(c.gemColorId))
            .OrderByDescending(c => c.count).ToList();
        return genInfosOrdered[0].gemColorId;
    }

    public virtual void SubstractOne(int colorId)
    {
        GenerationSettingsItem colorInfo = infos.First(c => c.gemColorId == colorId);
        colorInfo.count--;
    }

    protected void Add(int gemColorId, int count)
    {
        GenerationSettingsItem existing = infos.FirstOrDefault(c => c.gemColorId == gemColorId);
        if (existing == null)
        {
            existing = new GenerationSettingsItem();
            existing.gemColorId = gemColorId;
            infos.Add(existing);
        }
        existing.count += count;
    }

    private class GenerationSettingsItem
    {
        public int gemColorId;
        public int count;
    }
}
