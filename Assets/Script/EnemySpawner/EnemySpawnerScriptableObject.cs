using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VirusWave", menuName = "VirusSpawn")]
public class EnemySpawnerScriptableObject : ScriptableObject
{
    // List of Virus Type
    [SerializeField]
    public GameObject[] virus;

    // Spawn amount in 1 wave
    [SerializeField]
    public int spawnAmount = 5;

    // Delay between each virus
    [SerializeField]
    public float spawnDelay = 1f;

    // Number of virus spawn in one time
    [SerializeField]
    public int spawnInOneTime = 0;
}
