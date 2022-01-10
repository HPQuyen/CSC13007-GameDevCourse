using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour, IDamageable
{
    public class EntityLifeCircleEvent{
        public event Action onDied = delegate { };
        public event Action onSpawn = delegate { };

        public void NotifyOnDiedEvent() => onDied?.Invoke();
        public void NotifyOnSpawnEvent() => onSpawn?.Invoke();
    }

    [SerializeField]
    protected AIPath mAIPath;
    [SerializeField]
    protected Seeker mSeeker;
    [SerializeField]
    protected AIDestinationSetter mAIDestinationSetter;
    [SerializeField]
    protected Health mHealth;

    protected EntityLifeCircleEvent mLifeCircleEvent = new EntityLifeCircleEvent();
    public EntityLifeCircleEvent LifeCircleEvent => mLifeCircleEvent;
    //protected IAttackStrategy mAttackStrategy;


    public abstract void OnTakeDamage(float damage);
    public abstract void SetSpawnPosition(Vector3 position);
    protected abstract void OnDied();
}
