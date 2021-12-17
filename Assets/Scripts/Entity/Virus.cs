using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VirusType
{
    Alpha,
    Beta,
    Gamma,
    Delta
}
public class Virus : Entity
{
    public override void OnTakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
    public override void SetSpawnPosition(Vector3 position)
    {
        transform.position = position;
    }
}
