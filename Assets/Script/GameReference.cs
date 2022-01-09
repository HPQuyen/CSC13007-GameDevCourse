using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReference : Singleton<GameReference>
{
    public ScriptableObjectReference scriptableObjectReference;
    public PrefabReference prefabReference;
    public Lung lung;
}
