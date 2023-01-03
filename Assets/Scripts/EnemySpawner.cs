using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<waveConfig> waveConfigs = new();
    [SerializeField] float timeBetweenWaves = 0f;
    waveConfig currentWave;
    [SerializeField] bool isLooping;

    private void Start()
    {
        StartCoroutine (SpawnEnemyWaves());
    }

    // Spawn Enemies as per various path prefabs with random duration
    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (waveConfig wave in waveConfigs)
            {
                currentWave = wave;

                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0f,0f,180f), transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);

    }

    // Gives current wave for enemies to spawn
    public waveConfig GetCurrentWave()
    {
        return currentWave;
    }
}
