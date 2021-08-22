using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAllCollidersOnObjects : MonoBehaviour {

    
    public GameObject[] otherObjects;
    public bool ignoreAllChildColliders = true;
	void Awake ()
    {
        List<Collider> cs = new List<Collider>();
        foreach (GameObject otherObject in otherObjects)
        {
            Collider[] colliders = otherObject.GetComponents<Collider>();
            foreach (Collider collider1 in colliders)
            {
                cs.Add(collider1);
            }
        } 
        //
        Collider[] cs2 = GetComponentsInChildren<Collider>();
        //
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
 
}
