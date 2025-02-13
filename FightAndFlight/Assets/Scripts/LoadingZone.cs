using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject connectedLoadingZone;
    [SerializeField] private Vector3 enterOffset;
    [SerializeField] private int nextRoomIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.LoadNextRoom(connectedLoadingZone.transform.position + enterOffset, nextRoomIndex);
        }
    }
}
