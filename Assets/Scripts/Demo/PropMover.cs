using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMover : MonoBehaviour
{ 
    [SerializeField] private float moveSpeed = 5;
    //
    public KeyCode keyCodeUp = KeyCode.UpArrow;
    public KeyCode keyCodeDown = KeyCode.DownArrow;
    public KeyCode keyCodeLeft = KeyCode.LeftArrow;
    public KeyCode keyCodeRight = KeyCode.RightArrow;
    // 
    void Update()
    {
        Vector3 direction = Vector3.zero;
        // 
        if (Input.GetKey(keyCodeUp))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(keyCodeDown))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(keyCodeLeft))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(keyCodeRight))
        {
            direction += Vector3.right;
        }
        //
        transform.position += direction * moveSpeed * Time.deltaTime;
        // 
    }
    
}
