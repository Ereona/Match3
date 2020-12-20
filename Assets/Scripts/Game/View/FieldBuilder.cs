using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldBuilder : MonoBehaviour
{
    public float oneCellSize = 100;
    public RectTransform CellsParent;
    public RectTransform GemsParent;

    private List<CellObject> cellObjects = new List<CellObject>();
    private List<GemObject> gemObjects = new List<GemObject>();

    private int rowsCount;
    private int colsCount;

    public void BuildField(Field field)
    {
        rowsCount = field.Rows;
        colsCount = field.Cols;
        ObjectsStorage objects = FindObjectOfType<ObjectsStorage>();
        foreach (Cell c in field.GetAllCells())
        {
            CellObject cellPrefab = objects.CellPrefab;
            CellSO cellAsset = objects.GetCellOfType(c.type);
            CellObject cellObject = Instantiate(cellPrefab.gameObject).GetComponent<CellObject>();
            cellObject.SourceElement = c;
            cellObject.ApplySettings(cellAsset);
            cellObject.transform.SetParent(CellsParent.transform);
            cellObject.transform.SetAsLastSibling();
            cellObject.transform.localScale = Vector3.one;
            cellObject.transform.localPosition = CalcPosition(c.row, c.col);
            ((RectTransform)cellObject.transform).sizeDelta = Vector2.one * oneCellSize;
            cellObjects.Add(cellObject);
            if (c.GemInCell != null)
            {
                GemObject gemPrefab = objects.GemPrefab;
                GemSO gemAsset = objects.GetGemOfColor(c.GemInCell.colorId);
                GemObject gemObject = Instantiate(gemPrefab.gameObject).GetComponent<GemObject>();
                gemObject.SourceElement = c.GemInCell;
                gemObject.ApplySettings(gemAsset);
                gemObject.transform.SetParent(GemsParent.transform);
                gemObject.transform.SetAsLastSibling();
                gemObject.transform.localScale = Vector3.one;
                gemObject.transform.localPosition = cellObject.transform.localPosition;
                ((RectTransform)cellObject.transform).sizeDelta = Vector2.one * oneCellSize;
                gemObjects.Add(gemObject);
            }
        }
    }

    private Vector3 CalcPosition(int row, int col)
    {
        float x = (col - (float)(colsCount - 1) / 2) * oneCellSize;
        float y = (row - (float)(rowsCount - 1) / 2) * oneCellSize;
        return new Vector3(x, y, 0);
    }
}
