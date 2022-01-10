using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 10f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Ouchhh! {gameObject.name} took damage - current health: {health}...");

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
