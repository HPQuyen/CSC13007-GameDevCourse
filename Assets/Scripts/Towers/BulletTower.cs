using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : Tower
{
    protected IPool<Bullet> mBulletPool;

    protected override void PerformMission()
    {
        throw new System.NotImplementedException();
    }
    protected override void StartLoopJob()
    {
        throw new System.NotImplementedException();
    }
    public override void Init(TowerInteractionUI towerInteractionUI)
    {
        base.Init(towerInteractionUI);
    }
}
