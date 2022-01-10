using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskAmmunitionStrategy : MonoBehaviour, IAmmunitionStrategy
{
    public void Perform(BulletStat bulletStat, Collider2D collider, Action callback)
    {
        var target = collider.GetComponent<IFreezable>();
        if (target == null)
            return;
        target.OnFreeze(bulletStat.GetAbilityStat<FreezeAbilityStat>().freezeTime);

        // TODO: Add effect here
        var effect = Instantiate(GameReference.Instance.prefabReference.maskBulletExplosionEffect, ObjectHolder.Instance.transform);
        var listContactPoints = new List<ContactPoint2D>();
        var numOfContactPoints = collider.GetContacts(listContactPoints);
        if (numOfContactPoints > 0)
            effect.transform.position = listContactPoints[0].point;
        else
            effect.transform.position = collider.transform.position;
        Destroy(effect.gameObject, AnimationDuration.MEDIUM);

        callback?.Invoke();
    }
}
