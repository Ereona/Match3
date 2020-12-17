using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldBuilder : MonoBehaviour
{
    public GridLayoutGroup CellsParent;

    public void BuildField(Field field)
    {
        ObjectsController objects = FindObjectOfType<ObjectsController>();
        CellsParent.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        CellsParent.constraintCount = field.Cols;
        foreach (Cell c in field.GetAllCells())
        {
            CellObject cellPrefab = objects.GetCellOfType(c.type);
            CellObject cellObject = Instantiate(cellPrefab.gameObject).GetComponent<CellObject>();
            cellObject.transform.SetParent(CellsParent.transform);
            cellObject.transform.SetAsLastSibling();
            cellObject.transform.localScale = Vector3.one;
        }
    }
}
