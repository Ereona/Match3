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
            if (i >= 0 && i < Rows && j >= 0 && j < Cols)
            {
                return cells[i, j];
            }
            else
            {
                return null;
            }
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

    public List<Cell> GetFilledCells()
    {
        return GetAllCells().Where(c => c.GemInCell != null).ToList();
    }

    public void Clear()
    {
        foreach (Cell c in GetAllCells())
        {
            c.GemInCell = null;
        }
    }

    public void Swap(Cell c1, Cell c2)
    {
        Gem temp = c1.GemInCell;
        c1.GemInCell = c2.GemInCell;
        c2.GemInCell = temp;
    }

    public void ClearCells(List<Cell> cells)
    {
        foreach (Cell c in cells)
        {
            c.GemInCell = null;
        }
    }
}
