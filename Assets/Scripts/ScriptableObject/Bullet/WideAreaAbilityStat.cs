using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class WideAreaAbilityStat : BulletAbilityStat
{
    public float range;

    public WideAreaAbilityStat(float range)
    {
        this.range = range;
    }
    public override void OnInspectorGUI()
    {
        range = EditorGUILayout.FloatField("Range", range);
    }
}
