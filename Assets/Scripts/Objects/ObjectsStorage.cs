using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsStorage : MonoBehaviour
{
    public CellObject CellPrefab;
    public List<CellSO> CellAssets = new List<CellSO>();
    public GemObject GemPrefab;
    public List<GemSO> GemAssets = new List<GemSO>();

    public CellSO GetCellOfType(CellType type)
    {
        return CellAssets.FirstOrDefault(c => c.Type == type);
    }

    public GemSO GetGemOfColor(int colorId)
    {
        return GemAssets.FirstOrDefault(c => c.ColorId == colorId);
    }
}
