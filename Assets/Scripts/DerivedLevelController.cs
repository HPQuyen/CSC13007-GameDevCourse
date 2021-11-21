using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DerivedLevelController : LevelController
{
    private bool isVictory;

    private void Start()
    {
        LevelEventHandler.AddNewActionEvent(LevelEventCode.OnWinLevel, OnWinLevel);
        LevelEventHandler.AddNewActionEvent(LevelEventCode.OnLoseLevel, OnLoseLevel);
    }
    private void OnWinLevel()
    {
        isVictory = true;
        base.EndLevel();
    }
    private void OnLoseLevel()
    {
        isVictory = false;
        base.EndLevel();
    }
    public override bool IsVictory()
    {
        return isVictory;
    }
}

[CustomEditor(typeof(DerivedLevelController))]
public class DerivedLevelControllerEditor : Editor
{
    public DerivedLevelController data
    {
        get
        {
            return target as DerivedLevelController;
        }
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Victory"))
            LevelEventHandler.Invoke(LevelEventCode.OnWinLevel);
        if (GUILayout.Button("Defeat"))
            LevelEventHandler.Invoke(LevelEventCode.OnLoseLevel);
        GUILayout.EndHorizontal();
    }
}