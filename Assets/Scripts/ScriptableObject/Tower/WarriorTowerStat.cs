using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorTowerStat", menuName = GameData.GameName + "/TowerStat/WarriorTower")]
public class WarriorTowerStat : TowerStat
{
    [ReadOnly]
    public WarriorType warriorType;

    protected override void OnValidateTowerType(TowerType towerType)
    {
        this.towerType = towerType;
        switch (towerType)
        {
            case TowerType.Antibody:
                warriorType = WarriorType.AntibodyWarrior;
                break;
            case TowerType.SanitizerAnitbody:
                warriorType = WarriorType.SanitizerAntibodyWarrior;
                break;
            case TowerType.SoapAntibody:
                warriorType = WarriorType.SoapAntibodyWarrior;
                break;
            case TowerType.AntibodyMask:
                warriorType = WarriorType.AntibodyMaskWarrior;
                break;
            default:
                break;
        }
    }
}
