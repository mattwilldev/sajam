using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnimation : MonoBehaviour
{

    private Quaternion originalRotation;

    [SerializeField] private Transform followDummy;
    private Transform followJoint;
    [HideInInspector] new public Rigidbody rigidbody;
    private ConfigurableJoint joint; 
     

    void Awake()
    {
        followJoint = FindChildRecursive(followDummy, name);
        originalRotation = followJoint.localRotation;
        joint = GetComponent<ConfigurableJoint>();
        rigidbody = GetComponent<Rigidbody>();

    }


    void FixedUpdate()
    {
        joint.targetRotation = Quaternion.Inverse(followJoint.localRotation) * originalRotation ; 
    }

    Transform FindChildRecursive(Transform t, string name)
    { 
        //
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).name == name)
            {
                return t.GetChild(i);
            }
            else
            {
                Transform foundChild = FindChildRecursive(t.GetChild(i), name);
                if (foundChild != null)
                {
                    return foundChild;
                }
            }
        } 
        return null;
    }
}
