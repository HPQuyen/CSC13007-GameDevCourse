using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : Tower
{
    protected IPool<Bullet> mBulletPool;

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
            case TowerType.Sanitizer:
                arrayTowerTypeUpgradeable = new TowerType[] { TowerType.SanitizerAnitbody, TowerType.SanitizerSoap, TowerType.SanitizerMask};
                break;
            case TowerType.Soap:
                arrayTowerTypeUpgradeable = new TowerType[] { TowerType.SanitizerSoap, TowerType.SoapAntibody, TowerType.SoapMask };
                break;
            case TowerType.Mask:
                arrayTowerTypeUpgradeable = new TowerType[] { TowerType.SanitizerMask, TowerType.SoapMask, TowerType.AntibodyMask };
                break;
            case TowerType.SanitizerSoap:
            case TowerType.SanitizerMask:
            case TowerType.SoapMask:
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
