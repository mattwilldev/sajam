using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainHeight : MonoBehaviour
{

    public float height = 1;
    new private Rigidbody rigidbody;
    public float force = 10;
    private float groundHeight = 0;
    private float originalMaintainHeight;
    public bool onlyPullUp = true;
    //
    private void Awake()
    {
        originalMaintainHeight = height;
    }
    //
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); 
    }
    public void SetHeightOffset(float offset)
    {
        height = originalMaintainHeight + offset;
    }
    //  
    void FixedUpdate()
    { 
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + transform.forward.WithY(0) * 0.2f, 0.2f, Vector3.down, out hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            groundHeight = hit.point.y; 
        }
        if (groundHeight + height < transform.position.y - 1)
        {
            return; // ** FALLING INTO ABYSS **
        }
        //
        float diff = (groundHeight + height) - transform.position.y;
        if (diff > 0 || !onlyPullUp)
        {
            rigidbody.AddForce(Vector3.up * Time.deltaTime * diff * force, ForceMode.Impulse);
        }
        // 
    }

}
