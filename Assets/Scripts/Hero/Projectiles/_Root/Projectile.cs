using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevCourse.Hero
{
    public abstract class Projectile : MonoBehaviour, ILaunchable
    {
        #region Serialized Fields

        [Header("General attributes")]
        [SerializeField]
        protected ProjectileStats stats;

        [Header("Events")]
        [SerializeField]
        protected GameEvent onEventHitTarget;

        #endregion


        #region Interfaces Implementation

        public abstract void Launch(GameObject hero, GameObject target);

        #endregion
    }
}