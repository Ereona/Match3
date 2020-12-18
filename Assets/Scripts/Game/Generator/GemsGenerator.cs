using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemsGenerator
{
    public List<Gem> Generate(List<Cell> fillingCells, GemGenerationSettings genInfo)
    {
        List<int> allColors = genInfo.GetAllColors();
        List<CellFillingInfo> cellsWithColors = new List<CellFillingInfo>();
        foreach (Cell c in fillingCells)
        {
            cellsWithColors.Add(new CellFillingInfo(c, allColors));
        }
        AddGuaranteedMove(cellsWithColors, genInfo);
        int count = cellsWithColors.Where(c => !c.IsFilled).Count();
        for (int i = 0; i < count; i++)
        {
            int minPossibleColorsCount = cellsWithColors.Where(c => !c.IsFilled).Min(c => c.PossibleColors.Count);
            List<CellFillingInfo> cellsWithMinCount = cellsWithColors
                .Where(c => !c.IsFilled && c.PossibleColors.Count == minPossibleColorsCount).ToList();
            int cellIndex = Random.Range(0, cellsWithMinCount.Count);
            CellFillingInfo cell = cellsWithMinCount[cellIndex];
            int preferredColor = genInfo.GetPreferredColor(cell.PossibleColors);
            cell.Fill(preferredColor);
            genInfo.SubstractOne(preferredColor);
            UpdatePossibleColors(cell, cellsWithColors);
        }
        return ConvertToGems(cellsWithColors);
    }

    private void AddGuaranteedMove(List<CellFillingInfo> cellsWithColors, GemGenerationSettings genInfo)
    {
        List<CellFillingInfo> possibleStartCells = new List<CellFillingInfo>(cellsWithColors);
        bool found = false;
        PossibleMovePatternsContainer patternsContainer = new PossibleMovePatternsContainer();
        while ((!found) && possibleStartCells.Count > 0)
        {
            int startCellIndex = Random.Range(0, possibleStartCells.Count);
            CellFillingInfo startCell = possibleStartCells[startCellIndex];
            possibleStartCells.RemoveAt(startCellIndex);
            List<PossibleMovePattern> patterns = patternsContainer.GetAllPatterns();
            while (!found && patterns.Count > 0)
            {
                int patternIndex = Random.Range(0, patterns.Count);
                PossibleMovePattern pattern = patterns[patternIndex];
                patterns.RemoveAt(patternIndex);
                List<CellFillingInfo> filling;
                if (TryFillPattern(startCell, cellsWithColors, pattern, out filling))
                {
                    FillWithPreferredColor(filling, genInfo, cellsWithColors);
                    found = true;
                }
            }
        }

    }

    private void UpdatePossibleColors(CellFillingInfo filled, List<CellFillingInfo> all)
    {
        List<CellFillingInfo> sameColor = all.Where(c => c.IsFilled && c.FilledColor == filled.FilledColor).ToList();
        CheckLockForColor(filled, 1, 0, 2, 0, sameColor, all);
        CheckLockForColor(filled, -1, 0, -2, 0, sameColor, all);
        CheckLockForColor(filled, 0, 1, 0, 2, sameColor, all);
        CheckLockForColor(filled, 0, -1, 0, -2, sameColor, all);
        CheckLockForColor(filled, 1, 0, -1, 0, sameColor, all);
        CheckLockForColor(filled, -1, 0, 1, 0, sameColor, all);
        CheckLockForColor(filled, 0, 1, 0, -1, sameColor, all);
        CheckLockForColor(filled, 0, -1, 0, 1, sameColor, all);
        CheckLockForColor(filled, 2, 0, 1, 0, sameColor, all);
        CheckLockForColor(filled, -2, 0, -1, 0, sameColor, all);
        CheckLockForColor(filled, 0, 2, 0, 1, sameColor, all);
        CheckLockForColor(filled, 0, -2, 0, -1, sameColor, all);
    }

    private void CheckLockForColor(CellFillingInfo filled, int rowShift, int colShift, int rowShiftIfLock, int colShiftIfLock, List<CellFillingInfo> sameColor, List<CellFillingInfo> allCells)
    {
        CellFillingInfo neighbor = sameColor.FirstOrDefault(c => c.Cell.row == filled.Cell.row + rowShift && c.Cell.col == filled.Cell.col + colShift);
        if (neighbor != null)
        {
            CellFillingInfo forLock = allCells.FirstOrDefault(c => c.Cell.row == filled.Cell.row + rowShiftIfLock && c.Cell.col == filled.Cell.col + colShiftIfLock);
            if (forLock != null)
            {
                forLock.PossibleColors.Remove(filled.FilledColor);
            }
        }
    }

    private List<Gem> ConvertToGems(List<CellFillingInfo> cells)
    {
        List<Gem> result = new List<Gem>();
        foreach (CellFillingInfo cell in cells)
        {
            if (cell.IsFilled)
            {
                Gem g = new Gem();
                g.row = cell.Cell.row;
                g.col = cell.Cell.col;
                g.colorId = cell.FilledColor;
                result.Add(g);
            }
        }
        return result;
    }

    private bool TryFillPattern(CellFillingInfo startCell, List<CellFillingInfo> allCells, PossibleMovePattern pattern, out List<CellFillingInfo> filling)
    {
        List<Vector2Int> positions = pattern.GetPattern();
        filling = new List<CellFillingInfo>();
        filling.Add(startCell);
        foreach (Vector2Int pos in positions)
        {
            CellFillingInfo cell = allCells.FirstOrDefault(c => c.Cell.row == startCell.Cell.row + pos.x && c.Cell.col == startCell.Cell.col + pos.y);
            if (cell != null && !cell.IsFilled)
            {
                filling.Add(cell);
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void FillWithPreferredColor(List<CellFillingInfo> filling, GemGenerationSettings genInfo, List<CellFillingInfo> allCells)
    {
        List<int> colors = filling[0].PossibleColors;
        for (int i = 1; i < filling.Count; i++)
        {
            colors = colors.Intersect(filling[i].PossibleColors).ToList();
        }
        int color = genInfo.GetPreferredColor(colors);
        foreach (CellFillingInfo cell in filling)
        {
            cell.Fill(color);
            genInfo.SubstractOne(color);
            UpdatePossibleColors(cell, allCells);
        }
    }

    class CellFillingInfo
    {
        public CellFillingInfo(Cell c, List<int> colors)
        {
            Cell = c;
            PossibleColors = new List<int>(colors);
        }
        public Cell Cell { get; private set; }
        public List<int> PossibleColors { get; private set; }

        public void Fill(int color)
        {
            IsFilled = true;
            FilledColor = color;
        }

        public bool IsFilled { get; private set; }
        public int FilledColor { get; private set; }
    }
}
