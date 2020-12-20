using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Cell")]
public class CellSO : ScriptableObject
{
    public CellType Type;
    public Sprite Sprite;
    public Color Color;
}
