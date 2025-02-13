using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<ScriptableRoom> rooms;

    public List<ScriptableRoom> GetRoomList()
    {
        return rooms;
    }
}
