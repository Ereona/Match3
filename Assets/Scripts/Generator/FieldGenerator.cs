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
            if (genInfo.fixedCount)
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
            else if (genInfo.fixedRow)
            {
                for (int j = 0; j < field.Cols; j++)
                {
                    if (field[genInfo.rowNumber, j].type == CellType.Simple)
                    {
                        field[genInfo.rowNumber, j].type = genInfo.type;
                    }
                }
            }
            else if (genInfo.fixedCol)
            {
                for (int i = 0; i < field.Rows; i++)
                {
                    if (field[i, genInfo.colNumber].type == CellType.Simple)
                    {
                        field[i, genInfo.colNumber].type = genInfo.type;
                    }
                }
            }
        }

        return field;
    }
}
