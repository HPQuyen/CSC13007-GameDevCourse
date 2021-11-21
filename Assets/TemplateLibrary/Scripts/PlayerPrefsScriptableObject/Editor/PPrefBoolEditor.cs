using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PPrefBool))]
public class PPrefBoolEditor : Editor
{
    private PPrefBool data
    {
        get
        {
            return target as PPrefBool;
        }
    }
    private bool setValue;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField($"Current Value: {data.Get()}");
        if (GUILayout.Button("Clear"))
            data.Clear();
        setValue = EditorGUILayout.Toggle("Set Value: ", setValue);
        if (GUILayout.Button("Set"))
            data.Set(setValue);
    }
}
