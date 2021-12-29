using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityStat : ScriptableObject
{
    public float attackRange = 3f;
    public float maxHealth = 100f;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public float fireRate = 1f;
}
