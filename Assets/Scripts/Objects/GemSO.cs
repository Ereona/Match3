using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Gem")]
public class GemSO : ScriptableObject
{
    public int ColorId;
    public Sprite Sprite;
    public Color Color;
}
