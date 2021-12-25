using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabReference", menuName = GameData.GameName + "/GameReference/PrefabReference")]
public class PrefabReference : ScriptableSingleton<PrefabReference>
{
    public Tower warriorTower;
    public Tower bulletTower;
}