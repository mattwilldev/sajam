using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovementForce : MonoBehaviour
{
    public float forwardForce = 0;
    private Rigidbody rigidBody;
    //
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (forwardForce < -0.001f || forwardForce > 0.001f)
        {
            rigidBody.AddForce(transform.forward.WithY(0).normalized * forwardForce * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
