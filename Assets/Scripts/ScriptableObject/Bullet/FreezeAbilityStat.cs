using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class FreezeAbilityStat : BulletAbilityStat
{
    public float freezeTime;

    public FreezeAbilityStat(float freezeTime)
    {
        this.freezeTime = freezeTime;
    }
    public override void OnInspectorGUI()
    {
        freezeTime = EditorGUILayout.FloatField("FreezeTime", freezeTime);
    }
}
