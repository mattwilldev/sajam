using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldUpright : MonoBehaviour {

    new private Rigidbody rigidbody;
    public float force = 10;
    public Vector3 offset = Vector3.up;
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 100;
    }
	
	// Update is called once per frame
	void Update () {
        rigidbody.AddForceAtPosition(Vector3.up * Time.deltaTime * force  , transform.TransformPoint(offset), ForceMode.Impulse);
        rigidbody.AddForceAtPosition(Vector3.up * Time.deltaTime * -force , transform.TransformPoint(-offset), ForceMode.Impulse);
    }
}
