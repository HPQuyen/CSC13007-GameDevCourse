using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TowerInteractionUI))]
public class EmptyLand : MonoBehaviour
{
    [SerializeField]
    private TowerInteractionUI mTowerInteractionUI;
    [SerializeField]
    private SpriteRenderer mSpriteRenderer;

    private static TowerType[] arrayTowerTypeUpgradeable = new TowerType[] { TowerType.Sanitizer, TowerType.Soap, TowerType.Antibody, TowerType.Mask };

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if(mTowerInteractionUI == null)
            mTowerInteractionUI = GetComponent<TowerInteractionUI>();
        if (mSpriteRenderer == null)
            mSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    #endif

    private void Awake()
    {
        mTowerInteractionUI.SetInteractions(CreateInteractions());
    }
    private IInteraction CreateInteractions()
    {
        var listInteractionData = new List<InteractionData>();

        for (int i = 0; i < 4; i++)
        {
            var towerStat = ScriptableObjectReference.instance.GetTowerStat(arrayTowerTypeUpgradeable[i]);
            var data = new InteractionData(() => ConstructTowerCallback(towerStat), towerStat.towerInfo);
            listInteractionData.Add(data);
        }
        return new SimpleInteraction(listInteractionData);
    }
    private void ConstructTowerCallback(TowerStat towerStat)
    {
        var tower = Instantiate(PrefabReference.instance.bulletTower, transform);
        tower.transform.localPosition = Vector3.up;
        tower.transform.DOLocalMoveY(tower.transform.localPosition.y + 1f, AnimationDuration.TINY).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);
        tower.transform.DOScale(new Vector3(0.8f, 1.2f, 1f), AnimationDuration.TINY).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash);
        tower.Init(towerStat, mTowerInteractionUI);
        Destroy(mSpriteRenderer);
        Destroy(this);
    }
}
