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
        ObjectsController objects = FindObjectOfType<ObjectsController>();
        foreach (Cell c in field.GetAllCells())
        {
            CellObject cellPrefab = objects.GetCellOfType(c.type);
            CellObject cellObject = Instantiate(cellPrefab.gameObject).GetComponent<CellObject>();
            cellObject.Row = c.row;
            cellObject.Col = c.col;
            cellObject.transform.SetParent(CellsParent.transform);
            cellObject.transform.SetAsLastSibling();
            cellObject.transform.localScale = Vector3.one;
            cellObject.transform.localPosition = CalcPosition(c.row, c.col);
            ((RectTransform)cellObject.transform).sizeDelta = Vector2.one * oneCellSize;
            cellObjects.Add(cellObject);
        }
    }

    public void BuildGems(List<Gem> gems)
    {
        ObjectsController objects = FindObjectOfType<ObjectsController>();
        foreach (Gem g in gems)
        {
            GemObject gemPrefab = objects.GetGemOfColor(g.colorId);
            GemObject gemObject = Instantiate(gemPrefab.gameObject).GetComponent<GemObject>();
            gemObject.Row = g.row;
            gemObject.Col = g.col;
            gemObject.transform.SetParent(GemsParent.transform);
            gemObject.transform.SetAsLastSibling();
            gemObject.transform.localScale = Vector3.one;
            CellObject cellForGem = cellObjects.Find(c => c.Row == g.row && c.Col == g.col);
            gemObject.transform.localPosition = CalcPosition(g.row, g.col);
            gemObjects.Add(gemObject);
        }
    }

    private Vector3 CalcPosition(int row, int col)
    {
        float x = (col - (float)(colsCount - 1) / 2) * oneCellSize;
        float y = (row - (float)(rowsCount - 1) / 2) * oneCellSize;
        return new Vector3(x, y, 0);
    }
}
