using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VirusStat", menuName = GameData.GameName + "/EntityStat/Virus")]
public class VirusStat : EntityStat
{
    public VirusType virusType;
    [DrawIf("CanDrawBulletType")]
    public BulletType bulletType;
    public float damage = 10f;

    private bool CanDrawBulletType()
    {
        switch (virusType)
        {
            case VirusType.Alpha:
            case VirusType.Gamma:
                return false;
            case VirusType.Beta:
            case VirusType.Delta:
                return true;
            default:
                break;
        }
        return false;
    }
}
