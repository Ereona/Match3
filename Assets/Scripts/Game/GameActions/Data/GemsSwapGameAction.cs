using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsSwapGameAction : TwoCellsGameAction
{
    public override void OnActionPerformed(Field fieldWithGems)
    {
        fieldWithGems.Swap(Cell1, Cell2);
    }
}
