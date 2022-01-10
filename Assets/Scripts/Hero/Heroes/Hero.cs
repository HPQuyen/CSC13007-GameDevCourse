using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevCourse.Hero
{
    public enum HeroState
    {
        IDLE,
        MOVING,
        ATTACKING,
        DISABLE
    }

    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Hero : MonoBehaviour
    {
        #region Constants

        private const string _ENEMY_LAYER_ = "Enemy";

        private int _ENEMY_LAYER_HASH_;

        #endregion


        #region Serialized Fields

        [Header("Basic Stats")]
        [SerializeField]
        protected HeroStats heroStats;

        [Header("Movement")]
        [SerializeField]
        protected Navigator navigator;

        [Header("Combat")]
        [SerializeField]
        protected Projectile projectileType;
        [SerializeField]
        protected HealthSystem healthSystem;
        [SerializeField]
        protected Animator mAnimator;
        [SerializeField]
        protected SpriteRenderer mSpriteRenderer;

        #endregion


        #region Protected Fields

        #region State

        protected HeroState state = HeroState.IDLE;
        protected HeroState State
        {
            get => state;
            set {
                state = value;
                switch (state)
                {
                    case HeroState.IDLE:
                        mAnimator.SetFloat("Speed", 0f);
                        break;
                    case HeroState.MOVING:
                        mAnimator.SetFloat("Speed", 1f);
                        break;
                    case HeroState.ATTACKING:
                        mAnimator.SetTrigger("Attack");
                        break;
                    case HeroState.DISABLE:
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Movement

        protected Vector3 currentPosition = Vector3.zero;
        protected Vector3 targetPosition = Vector3.zero;

        #endregion

        #region Combat

        protected CircleCollider2D detectionZone;
        protected float detectionZoneExpansionFactor = .5f;

        protected HashSet<Virus> selectedEnemies = new HashSet<Virus>();

        protected bool isHitable = true;
        protected float meleeAttackCooldownCounter = 0f;
        protected bool isShootable = true;
        protected float rangedAttackCooldownCounter = 0f;

        protected float disableCooldownCounter = 0f;

        #endregion

        #endregion


        #region MonoBehaviour Callbacks

        protected virtual void Awake()
        {
            // Initialize layers
            _ENEMY_LAYER_HASH_ = LayerMask.NameToLayer(_ENEMY_LAYER_);
        }

        protected virtual void Start()
        {
            if (TryGetComponent(out detectionZone))
            {
                detectionZone.radius = heroStats.rangeAttackRadius + detectionZoneExpansionFactor;
            }

            healthSystem.SetMaxHealth(heroStats.maxHealth);
            healthSystem.SetHealth(heroStats.maxHealth);
        }

        protected virtual void Update()
        {
            // =========== MOVEMENT ===========
            Move();

            // ===========  COMBAT  ===========
            Attack();
            Revive();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == _ENEMY_LAYER_HASH_ 
                && collision.TryGetComponent<Virus>(out var enemy))
            {
                if (!selectedEnemies.Contains(enemy))
                {
                    selectedEnemies.Add(enemy);
                }
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == _ENEMY_LAYER_HASH_ 
                && collision.TryGetComponent<Virus>(out var enemy))
            {
                if (selectedEnemies.Contains(enemy))
                {
                    selectedEnemies.Remove(enemy);
                }
            }
        }

        #region DEBUG ZONE

        private void OnDrawGizmos()
        {
            if (detectionZone != null)
            {
                UnityEditor.Handles.color = Color.yellow;
                UnityEditor.Handles.DrawWireDisc(
                    transform.position, Vector3.back, detectionZone.radius
                );
            }

            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(
                transform.position, Vector3.back, heroStats.rangeAttackRadius
            );

            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.DrawWireDisc(
                transform.position, Vector3.back, heroStats.meleeAttackRadius
            );
        }

        #endregion

        #endregion


        #region Private Callbacks

        #region Movement

        // ===========  Basic Movement  ===========
        protected virtual void Move()
        {
            if (state == HeroState.DISABLE) return;

            currentPosition = transform.position;

            if (InputProcessor.Instance.MouseRightClick)
            {
                State = HeroState.MOVING;

                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                navigator.NavigateTarget(currentPosition, targetPosition);
                var direction = (targetPosition - transform.position).normalized;
                mSpriteRenderer.flipX = direction.x < 0f;
            }

            navigator.GoToTarget(currentPosition, transform, heroStats.movementSpeed);

            if (Vector2.Distance(currentPosition, targetPosition) < 0.5f)
                State = HeroState.IDLE;
        }

        #endregion

        #region Combat

        // ===========  Attack  ===========
        protected virtual void Attack()
        {
            if (state == HeroState.DISABLE || state == HeroState.MOVING) return;

            if (selectedEnemies.Count <= 0)
            {
                isHitable = true;
                meleeAttackCooldownCounter = 0f;
                isShootable = true;
                rangedAttackCooldownCounter = 0f;

                State = HeroState.IDLE;

                return;
            }

            State = HeroState.ATTACKING;

            if (selectedEnemies.Any(
                    enemy =>
                        Vector3.Distance(
                            transform.position, enemy.transform.position
                        ) <= heroStats.rangeAttackRadius
                    )
                )
            {
                // Shoot
                var selectedEnemy =
                        selectedEnemies
                        .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                        .FirstOrDefault();

                Shoot(selectedEnemy);
            }
        }

        protected abstract void Hit(Virus enemy);
        protected abstract void Shoot(Virus enemy);

        // ===========  Cast Skills  ===========
        protected virtual void CastSkill()
        {

        }

        // ===========  Take Damage  ===========
        public virtual void TakeDamage(float damage)
        {
            if (state == HeroState.DISABLE) return;

            healthSystem.SetHealth(healthSystem.GetHealth() - damage);

            if (healthSystem.GetHealth() <= 0f)
            {
                Die();
            }
        }

        // ===========  Die  ===========
        protected virtual void Die()
        {
            State = HeroState.DISABLE;
        }

        // ===========  Revive  ===========
        protected virtual void Revive()
        {
            if (state != HeroState.DISABLE) return;

            disableCooldownCounter += Time.deltaTime;

            if (disableCooldownCounter >= 5f)
            {
                disableCooldownCounter = 0f;

                State = HeroState.IDLE;
            }
        }

        #endregion

        #endregion
    }
}