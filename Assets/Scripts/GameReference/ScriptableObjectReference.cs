using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class GameReference
{
    [CreateAssetMenu(fileName = "ScriptableObjectReference", menuName = GameData.GameName + "/GameReference/ScriptableObjectReference")]
    public class ScriptableObjectReference : ScriptableSingleton<ScriptableObjectReference>
    {
        [SerializeField]
        private List<TowerStat> listTowerStat = new List<TowerStat>();

        public TowerStat GetTowerStat(TowerType type)
        {
            foreach (var item in listTowerStat)
            {
                if (item.towerType == type)
                    return item;
            }
            return null;
        }
    }
}
