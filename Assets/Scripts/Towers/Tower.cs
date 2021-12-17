using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Sanitizer,
    Soap,
    Antibody,
    Mask,
    SanitizerSoap,
    SanitizerAnitbody,
    SanitizerMask,
    SoapAntibody,
    SoapMask,
    AntibodyMask
}
public abstract class Tower : MonoBehaviour
{
    protected TowerStat mStat;
    protected IInteraction mInteraction;

    protected abstract void PerformMission();
    protected abstract void StartLoopJob();
    public virtual IInteraction GetInteraction() => mInteraction;
}
