using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room", order = 1)]
public class ScriptableRoom : ScriptableObject
{
    public int enemyCount;
    public GameObject roomPrefab;
    public bool isActive;
}
