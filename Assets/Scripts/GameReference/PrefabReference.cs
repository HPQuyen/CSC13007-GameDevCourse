using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class GameReference
{
    [CreateAssetMenu(fileName = "PrefabReference", menuName = GameData.GameName + "/GameReference/PrefabReference")]
    public class PrefabReference : ScriptableSingleton<PrefabReference>
    {
        
    }
}