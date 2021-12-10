using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DecayAbilityStat : BulletAbilityStat
{
    public float decayTime;

    public DecayAbilityStat(float decayTime)
    {
        this.decayTime = decayTime;
    }
    public override void OnInspectorGUI()
    {
        decayTime = EditorGUILayout.FloatField("DecayTime", decayTime);
    }
}
