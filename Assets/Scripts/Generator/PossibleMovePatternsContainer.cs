using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleMovePatternsContainer
{
    public List<PossibleMovePattern> GetAllPatterns()
    {
        List<PossibleMovePattern> result = new List<PossibleMovePattern>();
        for (int type = 1; type <= 3; type++)
        {
            for (int rot = 0; rot < 4; rot++)
            {
                PossibleMovePattern p1 = new PossibleMovePattern(type, rot, false);
                result.Add(p1);

                PossibleMovePattern p2 = new PossibleMovePattern(type, rot, true);
                result.Add(p2);
            }
        }
        return result;
    }
}
