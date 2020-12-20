using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchesList
{
    private List<List<Cell>> matches = new List<List<Cell>>();

    public void TryAdd(List<Cell> cells)
    {
        if (cells.Count >= 3)
        {
            matches.Add(new List<Cell>(cells));
        }
        cells.Clear();
    }

    public List<Cell> GetAllCells()
    {
        return matches.SelectMany(c => c).Distinct().ToList();
    }
}
