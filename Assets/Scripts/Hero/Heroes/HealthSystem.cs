using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevCourse.Hero
{
    public class HealthSystem : MonoBehaviour
    {
        #region Private Fields

        private float maxHealth = 0f;
        private float currentHealth = 0f;

        #endregion


        #region Public Callbacks

        public void SetMaxHealth(float value)
        {
            maxHealth = value;
        }

        public void SetHealth(float value)
        {
            currentHealth = value;
        }

        public float GetHealth()
        {
            return currentHealth;
        }

        #endregion
    }
}