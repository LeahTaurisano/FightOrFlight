using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private GameObject player;
    [SerializeField] private EnemySpawner enemySpawner;

    private GameObject currentRoom;
    private ScriptableRoom currentRoomData;

    void Start()
    {
        currentRoomData = roomManager.GetRoomList()[0];
        currentRoom = Instantiate(currentRoomData.roomPrefab, Vector3.zero, Quaternion.identity);
    }

    public void LoadNextRoom(Vector3 position, int index)
    {
        Destroy(currentRoom);
        currentRoomData = roomManager.GetRoomList()[index];
        currentRoom = Instantiate(currentRoomData.roomPrefab, Vector3.zero, Quaternion.identity);
        player.transform.position = position;
        enemySpawner.SetCurrentRoom(currentRoomData);
    }
}
