using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevCourse.Hero
{
    [CreateAssetMenu(
        fileName = "ProjectileStats", menuName = "ScriptableObjects/Projectile/Stats", order = 1
    )]
    public class ProjectileStats : ScriptableObject
    {
        #region Public Fields

        [Header("General Attributes")]
        [SerializeField]
        [Range(0.5f, 3f)]
        public float innerSpeed;
        public float attackDamage;

        #endregion
    }
}
