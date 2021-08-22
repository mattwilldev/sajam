using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToBox : MonoBehaviour
{
    private Joint joint;
    //
    private void OnCollisionEnter(Collision collision)
    {
        if (enabled && joint == null)
        {
            if (collision.gameObject.CompareTag("Box"))
            {
                joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            }
        }
    }
    private void OnDisable()
    {
        Destroy(joint);
    }
}
