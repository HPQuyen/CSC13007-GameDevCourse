using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : Tower
{
    [SerializeField]
    private Transform initialBulletPoint;

    private bool isShootable = true;
    private BulletType bulletType;
    private IPool<Bullet> mBulletPool;

    private void Awake()
    {
        mBulletPool = FindObjectOfType<BulletPooling>();
        switch (mStat.towerType)
        {
            case TowerType.Sanitizer:
                bulletType = BulletType.Sanitizer;
                break;
            case TowerType.Soap:
                bulletType = BulletType.Soap;
                break;
            case TowerType.Mask:
                bulletType = BulletType.Mask;
                break;
            default:
                break;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, mStat.activeRange);
    }

    protected override void StartLoopJob()
    {
        if (!isShootable || !isActivate)
            return;
        isShootable = false;
        var colliders = Physics2D.OverlapCircleAll(transform.position, mStat.activeRange);
        foreach (var item in colliders)
        {
            var target = item.GetComponent<Virus>();
            if(target != null)
            {
                ShootTarget(target);
                break;
            }
        }
        StartCoroutine(CommonCoroutine.Delay(mStat.cooldownTime, false, () => isShootable = true));
    }
    private void ShootTarget(Virus target)
    {
        var bullet = mBulletPool.Create(bulletType);
        var direction = target.transform.position - initialBulletPoint.position;
        var angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        bullet.transform.position = initialBulletPoint.position;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.onHitTarget += () =>
        {
            bullet.gameObject.SetActive(false);
            mBulletPool.Return(bulletType, bullet);
        };
        bullet.gameObject.SetActive(true);
        bullet.Shoot(direction.normalized);
    }
    public override void Init(TowerInteractionUI towerInteractionUI, CoinUI coinUI)
    {
        base.Init(towerInteractionUI, coinUI);
    }
}
