using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour {

    public Collider otherCollider;
    public bool ignore = true;
	void Awake () {
        Physics.IgnoreCollision(otherCollider, GetComponentInChildren<Collider>(), ignore);

        
    }
	
	 
}
