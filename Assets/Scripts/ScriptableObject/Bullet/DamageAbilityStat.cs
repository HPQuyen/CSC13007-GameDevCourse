using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DamageAbilityStat : BulletAbilityStat
{
    public float damage;

    public DamageAbilityStat(float damage)
    {
        this.damage = damage;
    }
    public override void OnInspectorGUI()
    {
        damage = EditorGUILayout.FloatField("Damage", damage);
    }
}
