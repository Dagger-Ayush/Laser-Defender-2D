using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class waveConfig : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefab = new();
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float timeBetweenEnemySpawns = 1f;
    [SerializeField] private float spawnTimerVariance = 0f;
    [SerializeField] private float minimumSpawnTime = 0.2f;


    // Gives Starting point of wave (1st child gameobject of path prefab)
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    // Gives all the waypoints in the path prefab
    public List<Transform> GetWayPoints()
    {
        List<Transform> wayPoints = new();

        foreach(Transform child in pathPrefab)
        {
            wayPoints.Add(child);
        }

        return wayPoints;
    }

    // Gives move speed of the enemy
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    // Gives number of enemies
    public int GetEnemyCount()
    {
        return enemyPrefab.Count;
    }

    // Gives enemy of that index
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefab[index];
    }

    // Gives random spawn time for enemies to spawn
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimerVariance, timeBetweenEnemySpawns + spawnTimerVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
