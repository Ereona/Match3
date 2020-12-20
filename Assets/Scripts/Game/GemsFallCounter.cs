using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemsFallCounter
{
    private Field FieldWithGems;

    public GemsFallCounter(Field field)
    {
        FieldWithGems = field;
    }

    public List<GameAction> DoMoving()
    {
        List<GameAction> result = new List<GameAction>();
        List<Cell> diagFalling = new List<Cell>();
        List<Cell> toFalling = new List<Cell>();
        for (int i = FieldWithGems.Rows - 1; i >= 0; i--)
        {
            for (int j = 0; j < FieldWithGems.Cols; j++)
            {
                if (!GameRules.NeedFillCell(FieldWithGems[i, j].type))
                {
                    continue;
                }
                if (FieldWithGems[i, j].GemInCell == null)
                {
                    GameAction moveAction = TryMove(FieldWithGems[i - 1, j], FieldWithGems[i, j]);
                    if (moveAction != null)
                    {
                        result.Add(moveAction);
                        toFalling.Add(FieldWithGems[i, j]);
                    }
                    else if (IsDiagonally(FieldWithGems[i, j]))
                    {
                        diagFalling.Add(FieldWithGems[i, j]);
                    }
                }
            }
        }
        foreach (Cell c in diagFalling)
        {
            int i = c.row;
            int j = c.col;
            if (toFalling.Contains(c))
            {
                continue;
            }
            GameAction moveAction = TryMove(FieldWithGems[i - 1, j - 1], FieldWithGems[i, j]);
            if (moveAction != null)
            {
                result.Add(moveAction);
                toFalling.Add(FieldWithGems[i, j]);
            }
            else
            {
                moveAction = TryMove(FieldWithGems[i - 1, j + 1], FieldWithGems[i, j]);
                if (moveAction != null)
                {
                    result.Add(moveAction);
                    toFalling.Add(FieldWithGems[i, j]);
                }
            }
        }
        return result;
    }

    private GameAction TryMove(Cell from, Cell to)
    {
        if (from != null)
        {
            if (GameRules.NeedFillCell(from.type))
            {
                Gem gem = from.GemInCell;
                if (gem != null)
                {
                    to.GemInCell = gem;
                    from.GemInCell = null;
                    MoveGemGameAction moveAction = new MoveGemGameAction();
                    moveAction.From = from;
                    moveAction.To = to;
                    moveAction.Gem = gem;
                    return moveAction;
                }
            }
        }
        return null;
    }

    private bool IsDiagonally(Cell to)
    {
        int i = to.row;
        int j = to.col;
        if (FieldWithGems[i - 1, j] != null)
        {
            if (!GameRules.NeedFillCell(FieldWithGems[i - 1, j].type))
            {
                return true;
            }
        }
        return false;
    }
}
