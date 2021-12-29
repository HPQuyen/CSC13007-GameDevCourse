using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTower : Tower
{
    protected IPool<Warrior> mWarriorPool;

    protected override void PerformMission()
    {
        throw new System.NotImplementedException();
    }
    protected override void StartLoopJob()
    {
        throw new System.NotImplementedException();
    }
    protected override IInteraction GetInteraction()
    {
        var listInteractionData = new List<InteractionData>();
        TowerType[] arrayTowerTypeUpgradeable = null;
        switch (mStat.towerType)
        {
            case TowerType.Antibody:
                arrayTowerTypeUpgradeable = new TowerType[] { TowerType.SanitizerAnitbody, TowerType.SoapAntibody, TowerType.AntibodyMask };
                break;
            case TowerType.SanitizerAnitbody:
            case TowerType.SoapAntibody:
            case TowerType.AntibodyMask:
                return null;
            default:
                break;
        }
        for (int i = 0; i < arrayTowerTypeUpgradeable.Length; i++)
        {
            var towerStat = ScriptableObjectReference.instance.GetTowerStat(arrayTowerTypeUpgradeable[i]);
            var data = new InteractionData(() => ConstructTowerCallback(towerStat), towerStat.towerInfo);
            listInteractionData.Add(data);
        }
        return new SimpleInteraction(listInteractionData);
    }
    public override void Init(TowerStat towerStat, TowerInteractionUI towerInteractionUI)
    {
        base.Init(towerStat, towerInteractionUI);
    }
}
