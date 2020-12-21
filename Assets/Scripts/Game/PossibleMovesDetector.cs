using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleMovesDetector
{
    private Field FieldWithGems;

    public PossibleMovesDetector(Field field)
    {
        FieldWithGems = field;
    }

    public bool MoveExists()
    {
        PossibleMovePatternsContainer patternsContainer = new PossibleMovePatternsContainer();
        List<PossibleMovePattern> patterns = patternsContainer.GetAllPatterns();
        foreach (PossibleMovePattern pattern in patterns)
        {
            if (SearchPattern(pattern))
            {
                return true;
            }
        }
        return false;
    }

    private bool SearchPattern(PossibleMovePattern pattern)
    {
        for (int i = 0; i < FieldWithGems.Rows; i++)
        {
            for (int j = 0; j < FieldWithGems.Cols; j++)
            {
                if (CheckPattern(pattern, i, j))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckPattern(PossibleMovePattern pattern, int i, int j)
    {
        int? firstColor = GetColor(i, j);
        if (!firstColor.HasValue)
        {
            return false;
        }
        foreach (Vector2Int v in pattern.GetPattern())
        {
            int? otherColor = GetColor(i + v.x, j + v.y);
            if (!otherColor.HasValue)
            {
                return false;
            }
            if (otherColor.Value != firstColor.Value)
            {
                return false;
            }
        }
        Vector2Int targetCellVector = pattern.GetTargetCell();
        if (!GetColor(i + targetCellVector.x, j + targetCellVector.y).HasValue)
        {
            return false;
        }
        return true;
    }

    private int? GetColor(int i, int j)
    {
        Cell cell = FieldWithGems[i, j];
        if (cell == null)
        {
            return null;
        }
        Gem gem = cell.GemInCell;
        if (gem == null)
        {
            return null;
        }
        return gem.colorId;
    }
}
