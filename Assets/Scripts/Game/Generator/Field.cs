using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Field
{
    public Field(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        FillSimpleCells();
    }

    private void FillSimpleCells()
    {
        cells = new Cell[Rows, Cols];
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                cells[i, j] = new Cell();
                cells[i, j].type = CellType.Simple;
                cells[i, j].row = i;
                cells[i, j].col = j;
            }
        }
    }

    private Cell[,] cells;
    public int Rows { get; private set; }
    public int Cols { get; private set; }

    public Cell this[int i, int j]
    {
        get
        {
            return cells[i, j];
        }
    }

    public List<Cell> GetSimpleCells()
    {
        List<Cell> result = new List<Cell>();
        foreach (Cell c in cells)
        {
            if (c.type == CellType.Simple)
            {
                result.Add(c);
            }
        }
        return result;
    }

    public List<Cell> GetAllCells()
    {
        return cells.Cast<Cell>().ToList();
    }
}
