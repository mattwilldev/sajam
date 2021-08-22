using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    public Transform target;
    new private Rigidbody rigidbody;
    public float force = 10;
    public Vector3 offset = Vector3.forward;
    public bool faceMainCamera = false;
    public bool faceDirection = false;
    public Vector3 direction;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 100;
        if (faceMainCamera)
        {
            target = Camera.main.transform;
        }
    }

    
    void Update()
    {
        Vector3 forward;

        if (faceDirection)
        {

            forward = direction;
        }
        else
        {
            forward = (target.position - transform.position).normalized;
        }
        rigidbody.AddForceAtPosition(forward * Time.deltaTime * force, transform.TransformPoint(offset), ForceMode.Impulse);
        rigidbody.AddForceAtPosition(-forward * Time.deltaTime * force, transform.TransformPoint(-offset), ForceMode.Impulse);
    }
}
