using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, smoothTime);
            float leftLimit = leftWall.transform.position.x;
            float rightLimit = rightWall.transform.position.x;
            float topLimit = topWall.transform.position.y;
            float bottomLimit = bottomWall.transform.position.y;
            float cameraHeight = Camera.main.orthographicSize;
            float cameraWidth = cameraHeight * Camera.main.aspect;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit + cameraWidth, rightLimit - cameraWidth),
                                             Mathf.Clamp(transform.position.y, bottomLimit + cameraHeight, topLimit - cameraHeight),
                                             transform.position.z);
        }
    }
}
