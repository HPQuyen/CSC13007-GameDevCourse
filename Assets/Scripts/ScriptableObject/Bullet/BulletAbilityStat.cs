using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum BulletAbilityStatType
{
    DamageAbility,
    DecayAbility,
    WideAreaAbility,
    FreezeAbility
}
[Serializable]
public abstract class BulletAbilityStat : ScriptableObject, IDerivedClassCustomEditor
{
    public abstract void OnInspectorGUI();
}
