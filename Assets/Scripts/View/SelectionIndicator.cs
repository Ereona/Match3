using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectionIndicator : MonoBehaviour
{
    public GameSettingsSO GameSettings;
    public CellEventChannelSO SelectionChangedEvent;
    public Image SelectionImage;
    private FieldObjectsContainer Objects;

    void Start()
    {
        SelectionImage.rectTransform.sizeDelta = Vector2.one * GameSettings.CellSize;
        SelectionImage.gameObject.SetActive(false);
        Objects = FindObjectOfType<FieldObjectsContainer>();
    }

    private void OnEnable()
    {
        SelectionChangedEvent.OnEventRaised += OnSelectionChanged;
    }

    private void OnDisable()
    {
        SelectionChangedEvent.OnEventRaised -= OnSelectionChanged;
    }

    private void OnSelectionChanged(Cell cell)
    {
        CellObject selected = null;
        if (cell != null)
        {
            selected = Objects.Cells.Find(cell);
        }
        if (selected != null)
        {
            SelectionImage.gameObject.SetActive(true);
            SelectionImage.transform.localPosition = selected.transform.localPosition;
        }
        else
        {
            SelectionImage.gameObject.SetActive(false);
        }
    }
}
