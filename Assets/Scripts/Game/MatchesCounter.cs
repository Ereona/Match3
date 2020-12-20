using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesCounter
{
    private Field FieldWithGems;

    public MatchesCounter(Field field)
    {
        FieldWithGems = field;
    }

    public bool AreNeighbors(Cell c1, Cell c2)
    {
        if (c1.row == c2.row && Mathf.Abs(c1.col - c2.col) == 1)
        {
            return true;
        }
        if (c1.col == c2.col && Mathf.Abs(c1.row - c2.row) == 1)
        {
            return true;
        }
        return false;
    }

    public bool HasMatchesAfterSwap(Cell cell1, Cell cell2)
    {
        if (cell1.GemInCell == null || cell2.GemInCell == null)
        {
            return false;
        }
        if (cell1.GemInCell.colorId == cell2.GemInCell.colorId)
        {
            return false;
        }
        FieldWithGems.Swap(cell1, cell2);
        bool result = CheckMatchWithCell(cell1) || CheckMatchWithCell(cell2);
        FieldWithGems.Swap(cell1, cell2);
        return result;
    }

    private bool CheckMatchWithCell(Cell cell)
    {
        return CheckMatchHorizontal(cell) || CheckMatchVertical(cell);
    }

    private bool CheckMatchHorizontal(Cell cell)
    {
        int color = cell.GemInCell.colorId;
        int sameColor = 0;
        int i = 1;
        while (IsSameColor(cell.row, cell.col + i, color))
        {
            i++;
            sameColor++;
        }
        i = 1;
        while (IsSameColor(cell.row, cell.col - i, color))
        {
            i++;
            sameColor++;
        }
        if (sameColor >= 2)
        {
            return true;
        }
        return false;
    }

    private bool CheckMatchVertical(Cell cell)
    {
        int color = cell.GemInCell.colorId;
        int sameColor = 0;
        int i = 1;
        while (IsSameColor(cell.row + i, cell.col, color))
        {
            i++;
            sameColor++;
        }
        i = 1;
        while (IsSameColor(cell.row - i, cell.col, color))
        {
            i++;
            sameColor++;
        }
        if (sameColor >= 2)
        {
            return true;
        }
        return false;
    }

    private bool IsSameColor(int row, int col, int color)
    {
        Cell cell = FieldWithGems[row, col];
        if (cell == null)
        {
            return false;
        }
        if (cell.GemInCell == null)
        {
            return false;
        }
        return cell.GemInCell.colorId == color;
    }


}
