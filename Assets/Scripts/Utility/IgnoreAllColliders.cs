using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAllColliders : MonoBehaviour {

    public GameObject colliders;
    public bool ignoreAllChildColliders = true;
	void Awake () {
        Collider[] cs = colliders.GetComponentsInChildren<Collider>();
        Collider[] cs2 = GetComponentsInChildren<Collider>();
        foreach (Collider otherCollider in cs)
        {
            if (ignoreAllChildColliders)
            {
                foreach (Collider thisCollider in cs2)
                {
                    Physics.IgnoreCollision(otherCollider, thisCollider, true);
                }
            }
            else
            {
                Physics.IgnoreCollision(otherCollider, GetComponentInChildren<Collider>(), true);
            }
        }

    }

    public static void IgnoreColliders(GameObject g1, GameObject g2)
    {
        Collider[] cs = g1.GetComponentsInChildren<Collider>();
        Collider[] cs2 = g2.GetComponentsInChildren<Collider>();
        foreach (Collider otherCollider in cs)
        {
            foreach (Collider thisCollider in cs2)
            {
                Physics.IgnoreCollision(otherCollider, thisCollider, true);
            }
        }
    }
}
