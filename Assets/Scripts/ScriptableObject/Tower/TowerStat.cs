using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerStat : ScriptableObject
{
    public TowerInfo towerInfo;
    public float activeRange;
    public float cooldownTime;
    public int constructPrice;
    public int destroyPrice;
    public TowerType towerType;

    protected void OnValidate()
    {
        OnValidateTowerType(towerType);
    }
    protected abstract void OnValidateTowerType(TowerType towerType);
}
