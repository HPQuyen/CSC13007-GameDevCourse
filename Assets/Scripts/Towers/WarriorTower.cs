﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTower : Tower
{
    protected IPool<Warrior> mWarriorPool;

    protected override void PerformMission()
    {
        throw new System.NotImplementedException();
    }
    protected override void StartLoopJob()
    {
        throw new System.NotImplementedException();
    }
    public override void Init(TowerInteractionUI towerInteractionUI, CoinUI coinUI)
    {
        base.Init(towerInteractionUI, coinUI);
    }
}
