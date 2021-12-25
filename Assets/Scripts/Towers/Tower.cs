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
    protected TowerStat mStat;
    protected TowerInteractionUI mTowerInteractionUI;

    protected virtual void OnValidate()
    {
        if (mSpriteRenderer == null)
            mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected abstract void PerformMission();
    protected abstract void StartLoopJob();
    protected abstract IInteraction GetInteraction();
    protected virtual void ConstructTowerCallback(TowerStat towerStat)
    {
        var tower = Instantiate(PrefabReference.instance.bulletTower, transform.parent);
        tower.transform.localPosition = Vector3.up;
        tower.transform.DOLocalMoveY(tower.transform.localPosition.y + 1f, AnimationDuration.TINY).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);
        tower.transform.DOScale(new Vector3(0.8f, 1.2f, 1f), AnimationDuration.TINY).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash);
        tower.Init(towerStat, mTowerInteractionUI);
        Destroy(gameObject);
    }
    public virtual void Init(TowerStat towerStat, TowerInteractionUI towerInteractionUI)
    {
        mStat = towerStat;
        mTowerInteractionUI = towerInteractionUI;
        mTowerInteractionUI.SetInteractions(GetInteraction());
        mSpriteRenderer.sprite = towerStat.towerInfo.towerSprite;
    }
}
