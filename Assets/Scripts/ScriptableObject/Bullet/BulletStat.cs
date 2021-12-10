using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletStat", menuName = GameData.GameName + "/BulletStat")]
public class BulletStat : ScriptableObject, ISerializationCallbackReceiver
{
    public BulletType bulletType;
    public Dictionary<BulletAbilityStatType, BulletAbilityStat> dictionaryAbilityStats = new Dictionary<BulletAbilityStatType, BulletAbilityStat>();

    [HideInInspector]
    public List<BulletAbilityStatType> listKeys = new List<BulletAbilityStatType>();
    [HideInInspector]
    public List<BulletAbilityStat> listValues = new List<BulletAbilityStat>();

    public BulletAbilityStat GetAbilityStat(BulletAbilityStatType type)
    {

        if (!dictionaryAbilityStats.ContainsKey(type))
            return null;
        return dictionaryAbilityStats[type];
    }
    public void OnBeforeSerialize()
    {
        listKeys.Clear();
        listValues.Clear();

        foreach (var item in dictionaryAbilityStats)
        {
            listKeys.Add(item.Key);
            listValues.Add(item.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        dictionaryAbilityStats = new Dictionary<BulletAbilityStatType, BulletAbilityStat>();
        for (int i = 0; i < listKeys.Count; i++)
            dictionaryAbilityStats.Add(listKeys[i], listValues[i]);
    }
}
