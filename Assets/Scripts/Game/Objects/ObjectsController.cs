using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    public List<CellObject> CellPrefabs = new List<CellObject>();

    public CellObject GetCellOfType(CellType type)
    {
        return CellPrefabs.FirstOrDefault(c => c.Type == type);
    }
}
