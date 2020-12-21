using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldBuilder : MonoBehaviour
{
    public GameSettingsSO GameSettings;
    public RectTransform CellsParent;
    public RectTransform GemsParent;

    private int rowsCount;
    private int colsCount;

    private ObjectsStorage Storage;
    private FieldObjectsContainer Objects;

    private void Start()
    {
        Storage = FindObjectOfType<ObjectsStorage>();
        Objects = FindObjectOfType<FieldObjectsContainer>();
    }

    public void BuildField(Field field)
    {
        rowsCount = field.Rows;
        colsCount = field.Cols;
        foreach (Cell c in field.GetAllCells())
        {
            CellObject cellPrefab = Storage.CellPrefab;
            CellSO cellAsset = Storage.GetCellOfType(c.type);
            CellObject cellObject = SimplePool.Spawn(cellPrefab.gameObject).GetComponent<CellObject>();
            cellObject.SourceElement = c;
            cellObject.ApplySettings(cellAsset);
            cellObject.transform.SetParent(CellsParent.transform);
            cellObject.transform.SetAsLastSibling();
            cellObject.transform.localScale = Vector3.one;
            cellObject.transform.localPosition = CalcPosition(c.row, c.col);
            ((RectTransform)cellObject.transform).sizeDelta = Vector2.one * GameSettings.CellSize;
            if (c.GemInCell != null)
            {
                BuildGemInCell(c, cellObject);
            }
        }
    }

    public GemObject BuildGemInCell(Cell c)
    {
        if (c.GemInCell != null)
        {
            return BuildGemInCell(c, Objects.Cells.Find(c));
        }
        return null;
    }

    private GemObject BuildGemInCell(Cell c, CellObject cellObject)
    {
        GemObject gemPrefab = Storage.GemPrefab;
        GemSO gemAsset = Storage.GetGemOfColor(c.GemInCell.colorId);
        GemObject gemObject = SimplePool.Spawn(gemPrefab.gameObject).GetComponent<GemObject>();
        gemObject.SourceElement = c.GemInCell;
        gemObject.ApplySettings(gemAsset);
        gemObject.transform.SetParent(GemsParent.transform);
        gemObject.transform.SetAsLastSibling();
        gemObject.transform.localScale = Vector3.one;
        gemObject.transform.localPosition = cellObject.transform.localPosition;
        ((RectTransform)gemObject.transform).sizeDelta = Vector2.one * GameSettings.CellSize;
        return gemObject;
    }

    private Vector3 CalcPosition(int row, int col)
    {
        float x = (col - (float)(colsCount - 1) / 2) * GameSettings.CellSize;
        float y = (row - (float)(rowsCount - 1) / 2) * GameSettings.CellSize;
        return new Vector3(x, y, 0);
    }
}
