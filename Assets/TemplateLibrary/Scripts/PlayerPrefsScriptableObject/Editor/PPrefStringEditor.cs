using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PPrefString))]
public class PPrefStringEditor : Editor
{
    private PPrefString data
    {
        get
        {
            return target as PPrefString;
        }
    }
    private string setValue;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField($"Current Value: {data.Get()}");
        if (GUILayout.Button("Clear"))
            data.Clear();
        setValue = EditorGUILayout.TextField("Set Value: ", setValue);
        if (GUILayout.Button("Set"))
            data.Set(setValue);
    }
}
