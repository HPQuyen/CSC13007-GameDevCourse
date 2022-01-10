using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevCourse.Hero
{
    [CreateAssetMenu(
        fileName = "HeroStats", menuName = "ScriptableObjects/Hero/Stats", order = 1
    )]
    public class HeroStats : ScriptableObject
    {
        #region Public Fields

        [Header("Movement")]
        public float movementSpeed;

        [Header("Damage")]
        public float meleeAttackDamage;

        [Header("Attack Range")]
        public float meleeAttackRadius;
        public float rangeAttackRadius;

        [Header("Fire Rate")]
        public float meleeFireRate;
        public float rangeFireRate;

        [Header("Blocking")]
        public float maxEnemiesBlockCount;

        [Header("Health")]
        public float maxHealth;

        #endregion
    }
}