using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellObject : FieldElementObject
{
    public Image CellImage;
    public Button Btn;
    public CellEventChannelSO CellClickedEvent;

    private void Start()
    {
        Btn.onClick.AddListener(OnButtonClick);
    }

    private void OnEnable()
    {
        FieldObjectsContainer objects = FindObjectOfType<FieldObjectsContainer>();
        if (objects != null)
        {
            objects.Cells.Add(this);
        }
    }

    private void OnDisable()
    {
        FieldObjectsContainer objects = FindObjectOfType<FieldObjectsContainer>();
        if (objects != null)
        {
            objects.Cells.Remove(this);
        }
    }

    public void ApplySettings(CellSO source)
    {
        CellImage.sprite = source.Sprite;
        CellImage.color = source.Color;
    }

    private void OnButtonClick()
    {
        CellClickedEvent.RaiseEvent(SourceElement as Cell);
    }
}
