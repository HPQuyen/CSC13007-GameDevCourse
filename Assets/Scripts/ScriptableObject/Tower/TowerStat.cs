using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerStat : ScriptableObject
{
    public InteractionInfo towerInfo;
    public float activeRange;
    public float cooldownTime;
    public float constructPrice;
    public float destroyPrice;
    public TowerType towerType;

    protected void OnValidate()
    {
        OnValidateTowerType(towerType);
    }
    protected abstract void OnValidateTowerType(TowerType towerType);
}
