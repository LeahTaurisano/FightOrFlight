using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ScriptableRoom currentRoom;
    [SerializeField] private float spawnTimer;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float distanceToPlayer;

    private int currentRoomEnemyCount;
    private float currentSpawnTimer;

    private void Start()
    {
        currentRoomEnemyCount = currentRoom.enemyCount;
    }

    private void Update()
    {
        if (currentRoomEnemyCount > 0)
        {
            currentSpawnTimer += Time.deltaTime;
            if (currentSpawnTimer > spawnTimer)
            {
                for (int i = 0; i < 5; ++i)
                {
                    float randomAngle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
                    Vector3 spawnPos = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * distanceToPlayer;
                    Instantiate(enemy, spawnPos, Quaternion.identity);
                    --currentRoomEnemyCount;
                }
                currentSpawnTimer = 0;
            }
        }
        else
        {
            currentRoom.isActive = false;
        }
    }

    public void SetCurrentRoom(ScriptableRoom room)
    {
        currentRoom = room;
        currentRoomEnemyCount = room.enemyCount;
    }
}
