using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PPrefInt))]
public class PPrefIntEditor : Editor
{
    private PPrefInt data
    {
        get
        {
            return target as PPrefInt;
        }
    }
    private int setValue;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField($"Current Value: {data.Get()}");
        if (GUILayout.Button("Clear"))
            data.Clear();
        setValue = EditorGUILayout.IntField("Set Value: ", setValue);
        if (GUILayout.Button("Set"))
            data.Set(setValue);
    }
}
