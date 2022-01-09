using DG.Tweening;
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
    [SerializeField]
    protected SpriteRenderer mSpriteRenderer;
    [SerializeField]
    protected TowerStat mStat;
    public TowerStat stat => mStat;

    protected TowerInteractionUI mTowerInteractionUI;

    protected virtual void OnValidate()
    {
        if (mSpriteRenderer == null)
            mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected abstract void PerformMission();
    protected abstract void StartLoopJob();
    protected virtual IInteraction GetInteraction() => null;
    public virtual void Init(TowerInteractionUI towerInteractionUI)
    {
        mTowerInteractionUI = towerInteractionUI;
        mTowerInteractionUI.SetInteractions(GetInteraction());
    }
}
