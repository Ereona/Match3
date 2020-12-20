using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGemGameAction : GameAction
{
    public Cell RemovingCell;

    public override void OnActionPerformed(Field fieldWithGems)
    {
        RemovingCell.GemInCell = null;
    }
}
