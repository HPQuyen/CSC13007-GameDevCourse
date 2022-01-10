using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour, IPool<Bullet>
{
    [SerializeField]
    private List<Bullet> bullets;

    private Dictionary<Enum, ObjectPooling<Bullet>> bulletPoolingDictionary;

    private void Awake()
    {
        bulletPoolingDictionary = new Dictionary<Enum, ObjectPooling<Bullet>>();
    }
    public Bullet Create(Enum type)
    {
        if (!bulletPoolingDictionary.ContainsKey(type))
        {
            var bulletPrefab = GetBulletOfType(type);
            bulletPoolingDictionary.Add(type, new ObjectPooling<Bullet>(() => {
                var bullet = Instantiate(bulletPrefab);
                bullet.gameObject.SetActive(false);
                return bullet;
            }, (item) => Destroy(item.gameObject)));
        }
        return bulletPoolingDictionary[type].Get();
    }

    private Bullet GetBulletOfType(Enum type)
    {
        foreach (var item in bullets)
        {
            if (item.stat.bulletType == (BulletType)type)
                return item;
        }
        return null;
    }
    public void Return(Enum type, Bullet item)
    {
        if (!bulletPoolingDictionary.ContainsKey(type) || item == null)
            return;
        item.gameObject.SetActive(false);
        bulletPoolingDictionary[type].Add(item);
    }
}
