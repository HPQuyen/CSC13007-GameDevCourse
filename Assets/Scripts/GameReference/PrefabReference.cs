using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabReference", menuName = GameData.GameName + "/GameReference/PrefabReference")]
public class PrefabReference : ScriptableObject
{
    public EmptyLand emptyLand;
    public GameObject sanitizerBulletExplosionEffect;
    public GameObject soapBulletExplosionEffect;
    public GameObject maskBulletExplosionEffect;

    [SerializeField]
    private List<Tower> listTower = new List<Tower>();

    public Tower GetTower(TowerType type)
    {
        foreach (var item in listTower)
        {
            if (item.stat.towerType == type)
                return item;
        }
        return null;
    }
}