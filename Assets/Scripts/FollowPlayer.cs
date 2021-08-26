using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate() {
        float x = (Mathf.Round(player.position.x * 100)) / 100.0f;
        float z = (Mathf.Round(player.position.z * 100)) / 100.0f;
        Vector3 cameraPosition = new Vector3(x, transform.position.y, z);

        Vector3 desiredPosition = cameraPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
