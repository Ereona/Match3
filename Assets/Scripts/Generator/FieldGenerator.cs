using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator
{
    public Field Generate(int rows, int cols, List<CellGenerationInfo> cellTypes)
    {
        Field field = new Field(rows, cols);
        List<Cell> simpleCells = field.GetSimpleCells();
        foreach (CellGenerationInfo genInfo in cellTypes)
        {
            for (int i = 0; i < genInfo.count; i++)
            {
                if (simpleCells.Count > 0)
                {
                    int index = Random.Range(0, simpleCells.Count);
                    Cell cell = simpleCells[index];
                    simpleCells.RemoveAt(index);
                    cell.type = genInfo.type;
                }
            }
        }
        return field;
    }
}
