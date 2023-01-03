using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    [SerializeField] waveConfig waveConfigSO;
    List<Transform> wayPoints;
    int wayPointsIndex = 0;


    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        waveConfigSO = enemySpawner.GetCurrentWave();
        wayPoints = waveConfigSO.GetWayPoints();
        transform.position = wayPoints[wayPointsIndex].position;
    }

    private void Update()
    {
        FollowPath();
    }

    // The Enemies follow the waypoints one by one and gets destroyed when tranversed through all of them
    private void FollowPath()
    {
        if (wayPointsIndex < wayPoints.Count)
        { 
            Vector3 targetPos = wayPoints[wayPointsIndex].position;
            float delta = waveConfigSO.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if (transform.position == targetPos)
                wayPointsIndex++;
        }
        else
            Destroy(gameObject);
    }
}
