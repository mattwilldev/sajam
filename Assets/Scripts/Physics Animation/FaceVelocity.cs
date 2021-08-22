using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceVelocity : MonoBehaviour
{
    
    new private Rigidbody rigidbody;
    public float force = 10;
    public float minimumSpeed = 4;
    public Vector3 offset = Vector3.up;
    private float spawnTime = 0;
  
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 100;
        spawnTime = Time.time;

    }

    
    void Update()
    {
        if (rigidbody.velocity.magnitude > minimumSpeed && Time.time - spawnTime > 0.2f)
        {
            Vector3 forward = rigidbody.velocity.normalized;
            rigidbody.AddForceAtPosition(forward * Time.deltaTime * force, transform.TransformPoint(offset), ForceMode.Impulse);
            rigidbody.AddForceAtPosition(-forward * Time.deltaTime * force, transform.TransformPoint(-offset), ForceMode.Impulse);
        }
    }
}
