using Pathfinding;
using System;
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
public enum VirusState
{
    None,
    Walk,
    Attack,
    TakeDamage,
    Died
}
public class Virus : Entity, IFreezable
{
    [SerializeField]
    protected Animator mAnimator;
    [SerializeField]
    protected EntityStat mStat;
    [SerializeField]
    protected SpriteRenderer mSpriteRenderer;
    [SerializeField]
    protected Collider2D mCollider2D;

    protected VirusState mState = VirusState.Walk;
    protected IVirusTarget attackTarget;
    protected Path path;
    protected int currentWayPoint;

    protected virtual void Update()
    {
        if (mState == VirusState.Died)
            return;
        switch (mState)
        {
            case VirusState.Walk:
                OnWalkState();
                break;
            case VirusState.Attack:
                OnAttackState();
                break;
            default:
                break;
        }
    }
    protected virtual void OnEnable()
    {
        mHealth.onDied += OnDied;
        mHealth.Init(mStat.maxHealth);
        mAIDestinationSetter.target = GameReference.Instance.lung.destination;
        mSeeker.StartPath(transform.position, mAIDestinationSetter.target.transform.position, OnCompletedPath);
    }
    protected virtual void OnDisable()
    {
        mHealth.onDied -= OnDied;
        mAIDestinationSetter.target = null;
    }

    
    public override void OnTakeDamage(float damage)
    {
        if (mState == VirusState.Died)
            return;
        var previousState = mState;
        mState = VirusState.TakeDamage;
        mAnimator.SetTrigger("OnHit");
        mHealth.TakeDamage(damage);
        StartCoroutine(CommonCoroutine.Delay(AnimationDuration.SSHORT, false, () => ReturnBackPreviousState(previousState)));
    }
    public override void SetSpawnPosition(Vector3 position)
    {
        transform.position = position;
        mLifeCircleEvent.NotifyOnSpawnEvent();
    }
    protected virtual void ReturnBackPreviousState(VirusState state)
    {
        if (state == VirusState.None || state == VirusState.TakeDamage || state == VirusState.Died)
            return;
        mState = state;
    }
    protected virtual void OnCompletedPath(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWayPoint = 0;
        }
    }
    protected override void OnDied()
    {
        mCollider2D.enabled = false;
        mState = VirusState.Died;
        mAnimator.SetTrigger("OnDied");
        mLifeCircleEvent.NotifyOnDiedEvent();
        StartCoroutine(CommonCoroutine.Delay(AnimationDuration.SHORT, false, () => gameObject.SetActive(false)));
        //Destroy(gameObject, AnimationDuration.SHORT);
    }
    protected virtual void OnWalkState()
    {
        if (mState == VirusState.TakeDamage || mState == VirusState.Died || mState == VirusState.None)
            return;

        // TODO:
        // - Find any Warrior || Player in attack range
        Debug.Log(mState);
        // - If not walk normally
        if (path == null)
            return;
        if(currentWayPoint >= path.vectorPath.Count)
            return;

        var direction = (Vector2) (path.vectorPath[currentWayPoint] - transform.position).normalized;
        var velocity = direction * mStat.moveSpeed * Time.deltaTime;
        var distance = Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]);

        if (distance < mAIPath.pickNextWaypointDist)
            currentWayPoint++;


        transform.position += (Vector3) velocity;
        mSpriteRenderer.flipX = Mathf.Sign(direction.x) < 0f;
    }
    protected virtual void OnAttackState() { }
    protected virtual void OnValidate()
    {
        if (mSeeker == null)
            mSeeker = GetComponent<Seeker>();
        if (mHealth == null)
            mHealth = GetComponent<Health>();
        if (mAIDestinationSetter == null)
            mAIDestinationSetter = GetComponent<AIDestinationSetter>();
        if (mAIPath == null)
            mAIPath = GetComponent<AIPath>();
        if (mSpriteRenderer == null)
            mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void OnFreeze(float freezeTime)
    {
        var previousState = mState;
        mState = VirusState.None;
        mAnimator.enabled = false;
        StartCoroutine(CommonCoroutine.Delay(freezeTime, false, () =>
        {
            mAnimator.enabled = true;
            ReturnBackPreviousState(previousState);
        }));
    }
}
