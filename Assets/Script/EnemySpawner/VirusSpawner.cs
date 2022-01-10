using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private EnemySpawnerScriptableObject[] enemyWaves;

    [SerializeField]
    private float waveDelayDefault = 10f;
    // private float waveDelayTimer;

    private int currentWave = 0;
    private int spawnVirusCount = 0; // Count Virus need to spawn
    
    // Count Virus left in wave
    // If it's reach zero, spawn new wave
    private float spawnVirusDelayDefault = 0;

    [SerializeField]
    private float spawnRadius = 1f;
    #endregion

    #region Unity Functions
    void Start()
    {
        StartCoroutine(CommonCoroutine.Delay(AnimationDuration.LONG, false, () =>
        {
            spawnVirusDelayDefault = enemyWaves[currentWave].spawnDelay;
            StartSpawningVirus();
        }));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
    #endregion

    public void StartSpawningVirus()
    {
        // Start Spawing Delay
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        // Spawn virus
        SpawningVirus();
        // Timer between each virus
        yield return new WaitForSeconds(spawnVirusDelayDefault);
        // Call again to loop
        StartCoroutine(SpawnDelay());
    }

    public void SpawningVirus()
    {
        // Random Type of Virus in the Wave to spawn
        int virusType = Random.Range(0, enemyWaves[currentWave].virus.Length);
        // Spawning each virus from the Wave
        if (spawnVirusCount < enemyWaves[currentWave].spawnAmount)
        {
            // Instantiate virus
            // Can be 1 virus at once
            // Can be 2 or more at once
            for (int idx = 1; idx <= enemyWaves[currentWave].spawnInOneTime; idx++)
            {
                Instantiate(enemyWaves[currentWave].virus[virusType], transform.position + RadiusSpawnPoint(), Quaternion.identity);
            }
            // Counting Virus to spawn
            spawnVirusCount += enemyWaves[currentWave].spawnInOneTime;
        } else
        {
            // TODO: Signal that all virus in Scene are destory
            
            // When there are still waves, continue spawning
            if (currentWave + 1 < enemyWaves.Length)
            {
                NewEnemyWaveAdvanced();
            }
            // Start Countdown for next wave
            StartCoroutine(CountdownBetweenWave());

        }

    }

    public void NewEnemyWaveAdvanced()
    {
        // Increment to the next wave
        // Reset counting the number of Virus
        // Get the Spawn Delay between each virus from the Scriptable Objecct
        // Reset the Wave Countdown Timer
        currentWave++;
        spawnVirusCount = 0;
        spawnVirusDelayDefault = enemyWaves[currentWave].spawnDelay;
    }

    private Vector3 RadiusSpawnPoint()
    {
        // Random Position in the spawning area
        return Random.insideUnitCircle * spawnRadius;
    }

    // Start countdown between wave
    IEnumerator CountdownBetweenWave() {
        yield return new WaitForSeconds(waveDelayDefault);
    }
}
