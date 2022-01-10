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
    [SerializeField]
    protected Sprite cancelSprite;

    public TowerStat stat => mStat;

    protected bool isActivate = false;
    protected TowerInteractionUI mTowerInteractionUI;
    protected CoinUI coinUI;

    protected virtual void OnValidate()
    {
        if (mSpriteRenderer == null)
            mSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected void Update()
    {
        StartLoopJob();
    }

    protected abstract void StartLoopJob();
    public virtual void Activate() => isActivate = true;
    public virtual void Init(TowerInteractionUI towerInteractionUI, CoinUI coinUI)
    {
        this.coinUI = coinUI;
        this.coinUI.OnChangedValueCoin(coinUI.CurrentCoin - mStat.constructPrice);
        mTowerInteractionUI = towerInteractionUI;
        mTowerInteractionUI.SetInteractions(GetInteraction());
    }
    protected virtual IInteraction GetInteraction(){
        //var listInteractionData = new List<InteractionData>();
        //var data = new InteractionData(OnDestroyTower, cancelSprite);
        //listInteractionData.Add(data);
        //return new SimpleInteraction(listInteractionData);
        return null;
    }
    protected virtual void OnDestroyTower()
    {
        coinUI.OnChangedValueCoin(coinUI.CurrentCoin + mStat.destroyPrice);
        var emptyLand = Instantiate(GameReference.Instance.prefabReference.emptyLand, ObjectHolder.Instance.transform);
        emptyLand.transform.position = transform.parent.position;
        emptyLand.transform.rotation = transform.parent.rotation;
        Destroy(transform.parent.gameObject);
        // TODO: Add effect here
    }
}
