using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Sanitizer,
    Soap,
    Mask,
    SanitizerSoap,
    SanitizerAnitbody,
    SanitizerMask,
    SoapAntibody,
    SoapMask,
    AntibodyMask
}
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private BulletStat mStat;
}
