using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletTowerStat", menuName = GameData.GameName + "/TowerStat/BulletTower")]
public class BulletTowerStat : TowerStat
{
    [ReadOnly]
    public BulletType bulletType;

    protected override void OnValidateTowerType(TowerType towerType)
    {
        this.towerType = towerType;
        switch (towerType)
        {
            case TowerType.Sanitizer:
                bulletType = BulletType.Sanitizer;
                break;
            case TowerType.Soap:
                bulletType = BulletType.Soap;
                break;
            case TowerType.Mask:
                bulletType = BulletType.Mask;
                break;
            case TowerType.SanitizerSoap:
                bulletType = BulletType.SanitizerSoap;
                break;
            case TowerType.SanitizerMask:
                bulletType = BulletType.SanitizerMask;
                break;
            case TowerType.SoapMask:
                bulletType = BulletType.SoapMask;
                break;
            default:
                break;
        }
    }
}
