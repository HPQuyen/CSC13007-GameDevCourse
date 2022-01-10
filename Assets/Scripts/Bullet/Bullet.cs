using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Sanitizer,
    Soap,
    Mask,
    SanitizerSoap,
    SanitizerAntibody,
    SanitizerMask,
    SoapAntibody,
    SoapMask,
    AntibodyMask
}
public class Bullet : MonoBehaviour
{
    public event Action onHitTarget = delegate { };
    [SerializeField]
    private Animator mAnimator;
    [SerializeField]
    private BulletStat mStat;
    [SerializeField]
    private Rigidbody2D mRigidbody2D;
    [SerializeField]
    private Collider2D mCollider2D;

    public BulletStat stat => mStat;

    private IAmmunitionStrategy ammunitionStrategy;

    protected void OnEnable()
    {
        if(ammunitionStrategy == null)
        {
            switch (mStat.bulletType)
            {
                case BulletType.Sanitizer:
                    ammunitionStrategy = gameObject.AddComponent<SanitizerAmmunitionStrategy>();
                    break;
                case BulletType.Soap:
                    ammunitionStrategy = gameObject.AddComponent<SoapAmmunitionStrategy>();
                    break;
                case BulletType.Mask:
                    ammunitionStrategy = gameObject.AddComponent<MaskAmmunitionStrategy>();
                    break;
                case BulletType.SanitizerSoap:
                    ammunitionStrategy = gameObject.AddComponent<SanitizerSoapAmmunitionStrategy>();
                    break;
                default:
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(mStat.targetTag))
            return;
        ammunitionStrategy.Perform(mStat, collision, OnHitTarget);
    }
    private void OnValidate()
    {
        if (mRigidbody2D == null)
            mRigidbody2D = GetComponent<Rigidbody2D>();
        if (mCollider2D == null)
            mCollider2D = GetComponent<Collider2D>();
    }

    private void OnHitTarget()
    {
        mRigidbody2D.velocity = Vector3.zero;
        mAnimator?.SetTrigger("OnHitTarget");
        onHitTarget?.Invoke();
        //StartCoroutine(CommonCoroutine.Delay(AnimationDuration.SSHORT, false, onHitTarget));
    }
    public void Shoot(Vector2 direction)
    {
        mRigidbody2D.AddForce(direction * mStat.force, ForceMode2D.Impulse);
    }
}
