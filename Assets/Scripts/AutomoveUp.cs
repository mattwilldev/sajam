using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomoveUp : MonoBehaviour
{
    public Transform player;
    public float autoMoveStartZ = 10;
    public float moveSpeed = 0.125f;

    void FixedUpdate() {
        Debug.Log("Player Z: " + player.position.z);
        if (player.position.z > autoMoveStartZ) {
            Vector3 cameraPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed);
            transform.position = cameraPosition;
        }
    }
}
