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

    private static TowerType[] arrayTowerTypeUpgradeable = new TowerType[] { TowerType.Sanitizer, TowerType.Soap, TowerType.SanitizerSoap, TowerType.Mask };

    private CoinUI coinUI;

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
        coinUI = FindObjectOfType<CoinUI>();
        mTowerInteractionUI.SetInteractions(CreateInteractions());
    }
    private IInteraction CreateInteractions()
    {
        var listInteractionData = new List<InteractionData>();

        for (int i = 0; i < arrayTowerTypeUpgradeable.Length; i++)
        {
            var towerStat = GameReference.Instance.prefabReference.GetTower(arrayTowerTypeUpgradeable[i]).stat;
            var data = new InteractionData(() => ConstructTowerCallback(towerStat), towerStat.towerInfo);
            listInteractionData.Add(data);
        }
        return new SimpleInteraction(listInteractionData);
    }
    private void ConstructTowerCallback(TowerStat towerStat)
    {
        if (coinUI.CurrentCoin < towerStat.constructPrice)
            return;
        var towerPrefab = GameReference.Instance.prefabReference.GetTower(towerStat.towerType);
        var tower = Instantiate(towerPrefab, transform);
        tower.transform.localScale = new Vector3(5.625f, 5.625f);
        tower.transform.localPosition = Vector3.zero;
        tower.transform.DOLocalMoveY(tower.transform.localPosition.y + 1f, AnimationDuration.TINY).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);
        tower.transform.DOScale(new Vector3(5.625f * 0.85f, 5.625f * 1.15f), AnimationDuration.TINY).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash).OnComplete(tower.Activate);
        tower.Init(mTowerInteractionUI, coinUI);
        Destroy(mSpriteRenderer);
        Destroy(this);
    }
}
