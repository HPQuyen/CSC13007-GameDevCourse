using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapAmmunitionStrategy : MonoBehaviour, IAmmunitionStrategy
{
    public void Perform(BulletStat bulletStat, Collider2D collider, Action callback)
    {
        var damage = bulletStat.GetAbilityStat<DamageAbilityStat>().damage;
        var range = bulletStat.GetAbilityStat<WideAreaAbilityStat>().range;
        var point = CalculateContactPoint(collider);
        var colliders = Physics2D.OverlapCircleAll(point, range);
        foreach (var item in colliders)
        {
            if (!item.CompareTag(bulletStat.targetTag))
                continue;
            var target = collider.GetComponent<IDamageable>();
            if (target == null)
                continue;
            target?.OnTakeDamage(damage);
        }

        // TODO: Add effect here
        var effect = Instantiate(GameReference.Instance.prefabReference.soapBulletExplosionEffect, ObjectHolder.Instance.transform);
        effect.transform.position = point;
        Destroy(effect.gameObject, AnimationDuration.MEDIUM);

        callback?.Invoke();
    }

    private Vector3 CalculateContactPoint(Collider2D collider)
    {
        var listContactPoints = new List<ContactPoint2D>();
        var numOfContactPoints = collider.GetContacts(listContactPoints);
        if (numOfContactPoints > 0)
            return listContactPoints[0].point;
        return collider.transform.position;
    }
}
