using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToParentRigidbody : MonoBehaviour
{
     
    void Awake()
    {
        ConfigurableJoint j = GetComponent<ConfigurableJoint>();
        j.connectedBody = transform.parent.GetComponentInParent<Rigidbody>();
    }

     
}
