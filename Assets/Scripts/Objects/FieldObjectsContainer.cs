using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectsContainer : MonoBehaviour
{
    public ObjectsContainer<CellObject> Cells = new ObjectsContainer<CellObject>();
    public ObjectsContainer<GemObject> Gems = new ObjectsContainer<GemObject>();
}
