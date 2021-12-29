using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour, IDamageable
{
    public class EntityLifeCircleEvent{
        public event Action onDied = delegate { };
        public event Action onSpawn = delegate { };

        public void NotifyOnDiedEvent() => onDied?.Invoke();
        public void NotifyOnSpawnEvent() => onSpawn?.Invoke();
    }

    protected Health mHealth;
    protected EntityLifeCircleEvent mLifeCircleEvent;
    public EntityLifeCircleEvent LifeCircleEvent => mLifeCircleEvent;
    //protected IAttackStrategy mAttackStrategy;
    //protected AIPath mAIPath;
    //protected AIDestinationSetter mAIDestinationSetter;

    public abstract void OnTakeDamage(float damage);
    public abstract void SetSpawnPosition(Vector3 position);
}
