using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToBall : MonoBehaviour
{
    public Transform playerTransform;
    public float thrust = 20f;
    public KeyCode keyCodeUp = KeyCode.UpArrow;
    public KeyCode keyCodeDown = KeyCode.DownArrow;
    public KeyCode keyCodeLeft = KeyCode.LeftArrow;
    public KeyCode keyCodeRight = KeyCode.RightArrow;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    void OnCollisionEnter(Collision col) {
        
        if (col.gameObject.tag == "Player") {
            
            Vector3 direction = Vector3.zero;

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

            rb.AddForce(direction * thrust, ForceMode.Impulse);
        }
    }

    
}
