using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;         // Reference to the player's transform
    public float smoothing = 5f;    // Smoothing factor for camera movement

    private Vector3 offset;         // Offset distance between the camera and player

    void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - player.position;
    }

    void Update()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = player.position + offset;

        // Update the camera position directly to center on the player
        transform.position = targetPosition;
    }
}
