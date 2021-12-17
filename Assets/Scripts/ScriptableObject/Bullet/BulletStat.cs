using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "BulletStat", menuName = GameData.GameName + "/BulletStat")]
public class BulletStat : ScriptableObject, ISerializationCallbackReceiver
{
    public BulletType bulletType;
    public Dictionary<BulletAbilityStatType, BulletAbilityStat> dictionaryAbilityStats = new Dictionary<BulletAbilityStatType, BulletAbilityStat>();

    [HideInInspector]
    public List<BulletAbilityStatType> listKeys = new List<BulletAbilityStatType>();
    [HideInInspector]
    public List<BulletAbilityStat> listValues = new List<BulletAbilityStat>();

    public T GetAbilityStat<T>() where T : BulletAbilityStat
    {
        var type = ConvertGenericTypeToEnum(typeof(T));
        if (!dictionaryAbilityStats.ContainsKey(type))
            return null;
        return (T) dictionaryAbilityStats[type];
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

    private BulletAbilityStatType ConvertGenericTypeToEnum(Type requestType)
    {
        if (requestType == typeof(DamageAbilityStat))
        {
            return BulletAbilityStatType.DamageAbility;
        }
        else if (requestType == typeof(DecayAbilityStat))
        {
            return BulletAbilityStatType.DecayAbility;
        }
        else if (requestType == typeof(WideAreaAbilityStat))
        {
            return BulletAbilityStatType.WideAreaAbility;
        }
        else if(requestType == typeof(FreezeAbilityStat))
        {
            return BulletAbilityStatType.FreezeAbility;
        }
        return BulletAbilityStatType.DamageAbility;
    }
}
