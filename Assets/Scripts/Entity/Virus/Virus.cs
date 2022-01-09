using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VirusType
{
    Alpha,
    Beta,
    Gamma,
    Delta
}
public class Virus : Entity
{
    [SerializeField]
    protected Animator mAnimator;

    protected virtual void OnEnable()
    {
        mHealth.onDied += OnDied;
        mAIDestinationSetter.target = GameReference.Instance.lung.transform;
    }
    protected virtual void OnDisable()
    {
        mHealth.onDied -= OnDied;
        mAIDestinationSetter.target = null;
    }
    public override void OnTakeDamage(float damage)
    {
        mHealth.TakeDamage(damage);
    }
    public override void SetSpawnPosition(Vector3 position)
    {
        transform.position = position;
        mLifeCircleEvent.NotifyOnSpawnEvent();
    }
    protected override void OnDied()
    {
        mLifeCircleEvent.NotifyOnDiedEvent();
    }
    protected virtual void OnValidate()
    {
        if (mHealth == null)
            mHealth = GetComponent<Health>();
        if (mAIDestinationSetter == null)
            mAIDestinationSetter = GetComponent<AIDestinationSetter>();
        if (mAIPath == null)
            mAIPath = GetComponent<AIPath>();
    }
}
