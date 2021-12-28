using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private VirusSpawner enemySpawner;

    public void OnClickStartSpawningVirus()
    {
        // On Click "Spawn Virus" Button
        // It wiil Start Spawning Virus
        enemySpawner.StartSpawningVirus();
    }
}
