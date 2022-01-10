using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTower : Tower
{
    protected IPool<Warrior> mWarriorPool;

    private void OnAwake()
    {
        
    }
    public override void Init(TowerInteractionUI towerInteractionUI, CoinUI coinUI)
    {
        base.Init(towerInteractionUI, coinUI);
    }

    protected override void StartLoopJob()
    {
        if (!isActivate)
            return;
    }
}
