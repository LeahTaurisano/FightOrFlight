using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : MonoBehaviour
{
    [SerializeField] private float fairyDistance;
    [SerializeField] private GameObject player;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = Vector3.Normalize(mousePos - player.transform.position);
        transform.position = player.transform.position + direction * fairyDistance;
    }
}
