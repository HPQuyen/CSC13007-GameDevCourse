using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PPrefFloat))]
public class PPrefFloatEditor : Editor
{
    private PPrefFloat data
    {
        get
        {
            return target as PPrefFloat;
        }
    }
    private float setValue;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField($"Current Value: {data.Get()}");
        if (GUILayout.Button("Clear"))
            data.Clear();
        setValue = EditorGUILayout.FloatField("Set Value: ", setValue);
        if (GUILayout.Button("Set"))
            data.Set(setValue);
    }
}
