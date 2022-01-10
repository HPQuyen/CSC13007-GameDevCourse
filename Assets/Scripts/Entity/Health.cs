using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    public Action onDied;

    private const float MIN_HEALTH = 0f;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private Slider healthSlider;

    private void Start()
    {
        if (healthSlider == null)
        {
            Debug.LogError("Health bar is null, please assign it");
            return;
        }
    }

    public void Init(float maxHealth)
    {
        if (healthSlider == null)
        {
            Debug.LogError("Health bar is null, please assign it");
            return;
        }
        currentHealth = maxHealth;
        healthSlider.minValue = MIN_HEALTH;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, MIN_HEALTH, healthSlider.maxValue);
        healthSlider.DOValue(currentHealth, AnimationDuration.SSHORT).SetEase(Ease.Linear);
        if (currentHealth == MIN_HEALTH)
            onDied?.Invoke();
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, MIN_HEALTH, healthSlider.maxValue);
        healthSlider.DOValue(currentHealth, AnimationDuration.SSHORT).SetEase(Ease.Linear);
    }
}
