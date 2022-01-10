using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevCourse.Hero
{
    public class Arrow : Projectile
    {
        #region Serialized Fields

        [Header("-----------------------", order = 0)]
        [Header("Arrow attributes", order = 1)]
        [SerializeField]
        [Range(5f, 20f)]
        private float shootingHeight;

        #endregion


        #region Private Fields

        private float bezierTime;

        private float startpointOffset = 0.5f;
        private float endpointOffset = 0.5f;

        private int targetGUID;

        #endregion


        #region MonoBehavior Callbacks

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) return;

            var collisionGUID = collision.transform.root.gameObject.GetInstanceID();

            if (collisionGUID == targetGUID)
            {
                onEventHitTarget.RaiseEventByID(collisionGUID);

                if (collision.TryGetComponent<Virus>(out var enemy))
                {
                    enemy.OnTakeDamage(stats.attackDamage);
                }
            }
        }

        #endregion


        #region Interfaces Implementation

        public override void Launch(GameObject hero, GameObject target)
        {
            targetGUID = target.transform.root.gameObject.GetInstanceID();

            var heroPosition = hero.transform.position + Vector3.up * startpointOffset;
            StartCoroutine(SimulateBallistic(heroPosition, target));
        }

        #endregion


        #region Private Methods

        private Vector2 Bezier2(Vector2 start, Vector2 control, Vector2 end, float t)
        {
            return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
        }

        private IEnumerator SimulateBallistic(Vector3 heroPosition, GameObject target)
        {
            do
            {
                // --- Position ---
                bezierTime += Time.deltaTime / stats.innerSpeed;

                // Target is no longer alive -> Nothing to do here!
                if (target == null) break;

                // Calculate control point position
                var controlPosition =
                    (heroPosition + target.transform.position) / 2f;

                var heightFactor = heroPosition.y > target.transform.position.y ?
                    (heroPosition.y - controlPosition.y) + shootingHeight
                    : (target.transform.position.y - controlPosition.y) + shootingHeight;

                controlPosition.y += heightFactor;

                // Calculate endpoint position
                var targetPosition = target.transform.position + Vector3.up * endpointOffset;

                var newPosition =
                    Bezier2(
                        heroPosition, controlPosition, targetPosition, bezierTime
                    );

                transform.position = newPosition;

                // --- Rotation ---
                var nextBezierTime = bezierTime + Time.deltaTime / stats.innerSpeed;
                var direction =
                   (Vector3)Bezier2(
                        heroPosition, controlPosition, targetPosition, nextBezierTime
                   ) - transform.position;

                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                yield return new WaitForEndOfFrame();

            } while (bezierTime < 1f);

            Destroy(gameObject);
        }

        #endregion
    }
}