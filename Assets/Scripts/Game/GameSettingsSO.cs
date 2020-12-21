using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettingsSO : ScriptableObject
{
    public int RowsCount;
    public int ColsCount;
    public int HolesCount;
    public List<int> Colors = new List<int>();
}
