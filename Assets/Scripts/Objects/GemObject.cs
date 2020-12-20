using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemObject : FieldElementObject
{
    public Image GemImage;

    public void ApplySettings(GemSO asset)
    {
        GemImage.sprite = asset.Sprite;
        GemImage.color = asset.Color;
    }

    private void OnEnable()
    {
        FieldObjectsContainer objects = FindObjectOfType<FieldObjectsContainer>();
        if (objects != null)
        {
            objects.Gems.Add(this);
        }
    }

    private void OnDisable()
    {
        FieldObjectsContainer objects = FindObjectOfType<FieldObjectsContainer>();
        if (objects != null)
        {
            objects.Gems.Remove(this);
        }
    }
}
