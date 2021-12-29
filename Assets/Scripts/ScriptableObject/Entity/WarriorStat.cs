using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorStat", menuName = GameData.GameName + "/EntityStat/Warrior")]
public class WarriorStat : EntityStat
{
    public WarriorType warriorType;
    [DrawIf("CanDrawBulletType")]
    public BulletType bulletType;
    [DrawIf("CanDrawDamage")]
    public float damage = 20f;

    private bool CanDrawBulletType()
    {
        switch (warriorType)
        {
            case WarriorType.AntibodyWarrior:
                return false;
            case WarriorType.SanitizerAntibodyWarrior:
            case WarriorType.SoapAntibodyWarrior:
            case WarriorType.AntibodyMaskWarrior:
                return true;
            default:
                break;
        }
        return false;
    }

    private bool CanDrawDamage() => !CanDrawBulletType();
}
