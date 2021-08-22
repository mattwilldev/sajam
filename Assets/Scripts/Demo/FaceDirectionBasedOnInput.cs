using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirectionBasedOnInput : MonoBehaviour
{
    [SerializeField] private FaceTarget faceTarget;
    //
    //
    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        if (direction != Vector3.zero)
        { 
            direction.Normalize();
            faceTarget.direction= Vector3.RotateTowards(faceTarget.direction, direction, Time.deltaTime * 6, Time.deltaTime * 6);
        }
    }
}
