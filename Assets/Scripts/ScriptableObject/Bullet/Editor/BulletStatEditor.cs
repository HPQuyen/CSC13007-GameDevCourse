using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BulletStat))]
public class BulletStatEditor : Editor
{
    private BulletStat data
    {
        get
        {
            return target as BulletStat;
        }
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawDirectionaryLayout();
        if (GUILayout.Button("Clear All"))
            ClearAll();
    }
    private void AddLast()
    {
        if (IsFull())
            return;
        BulletAbilityStatType type = Array.Find(Enum.GetValues(typeof(BulletAbilityStatType)) as BulletAbilityStatType[], item => !data.dictionaryAbilityStats.ContainsKey(item));
        AddAbilityStat(type);
    }
    private void RemoveLast()
    {
        if (data.dictionaryAbilityStats.Count <= 0)
            return;
        RemoveAbilityStat(data.dictionaryAbilityStats.Keys.Last());
    }
    private void ClearAll()
    {
        while(data.dictionaryAbilityStats.Count > 0)
            RemoveLast();
        data.dictionaryAbilityStats.Clear();
        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(data);
    }
    private void DrawDirectionaryLayout()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bullet Ability Stat", EditorStyles.centeredGreyMiniLabel);
        if (GUILayout.Button("+", GUILayout.ExpandWidth(false)))
            AddLast();
        if (GUILayout.Button("-", GUILayout.ExpandWidth(false)))
            RemoveLast();
        EditorGUILayout.EndHorizontal();
        foreach (var item in data.dictionaryAbilityStats.ToList())
        {
            EditorGUILayout.BeginHorizontal();
            var type = EditorGUILayout.EnumPopup(item.Key);
            item.Value.OnInspectorGUI();
            if ((BulletAbilityStatType)type != item.Key)
                OnValidateData(new ValueChangedData<BulletAbilityStatType>(item.Key, (BulletAbilityStatType)type));
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }
    private void OnValidateData(ValueChangedData<BulletAbilityStatType> changedData)
    {
        RemoveAbilityStat(changedData.oldValue);
        AddAbilityStat(changedData.newValue);
    }
    private void AddAbilityStat(BulletAbilityStatType type)
    {
        if (data.dictionaryAbilityStats.ContainsKey(type))
            return;
        CustomEditorUltilities.CreateFolder(CustomEditorUltilities.GetFolderAssetPath(data), data.name);
        string path = null;
        BulletAbilityStat bulletAbilityStat = null;
        switch (type)
        {
            case BulletAbilityStatType.DamageAbility:
                path = CustomEditorUltilities.GenerateAssetPathFromAsset<DamageAbilityStat>(data);
                bulletAbilityStat = CustomEditorUltilities.CreateScriptableObject<DamageAbilityStat>(path) as DamageAbilityStat;
                break;
            case BulletAbilityStatType.DecayAbility:
                path = CustomEditorUltilities.GenerateAssetPathFromAsset<DecayAbilityStat>(data);
                bulletAbilityStat = CustomEditorUltilities.CreateScriptableObject<DecayAbilityStat>(path) as DecayAbilityStat;
                break;
            case BulletAbilityStatType.WideAreaAbility:
                path = CustomEditorUltilities.GenerateAssetPathFromAsset<WideAreaAbilityStat>(data);
                bulletAbilityStat = CustomEditorUltilities.CreateScriptableObject<WideAreaAbilityStat>(path) as WideAreaAbilityStat;
                break;
            case BulletAbilityStatType.FreezeAbility:
                path = CustomEditorUltilities.GenerateAssetPathFromAsset<FreezeAbilityStat>(data);
                bulletAbilityStat = CustomEditorUltilities.CreateScriptableObject<FreezeAbilityStat>(path) as FreezeAbilityStat;
                break;
            default:
                break;
        }
        data.dictionaryAbilityStats.Add(type, bulletAbilityStat);
        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(data);
    }
    private void RemoveAbilityStat(BulletAbilityStatType key)
    {
        if (!data.dictionaryAbilityStats.ContainsKey(key))
            return;
        var abilityStat = data.dictionaryAbilityStats[key];
        if (!CustomEditorUltilities.DeleteScriptableObject(abilityStat))
        {
            Debug.LogWarning("Asset do not exist in Project");
            return;
        }
        data.dictionaryAbilityStats.Remove(key);
        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(data);
    }
    private bool IsFull()
    {
        var length = Enum.GetNames(typeof(BulletAbilityStatType)).Length;
        return data.dictionaryAbilityStats.Count == length;
    }
}
