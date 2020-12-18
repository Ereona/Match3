using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRules
{
    public static bool NeedFillCell(CellType type)
    {
        return type != CellType.Hole;
    }
}
