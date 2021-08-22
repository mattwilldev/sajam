using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainVerticalAlignment : MonoBehaviour
{

    [SerializeField] private Transform[] relativeTo;
    [SerializeField] private float alignmentForce = 20;
    //
    private float originalAlignmentForce = 20;
    private float enabledTime = 0;
    //
    public float alignmentForceM = 1;  // *** THIS GETS CONTROLLED BY THE CHARACTER CONTROLLER *** 
    //
    new  private Rigidbody rigidbody;
    //
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        originalAlignmentForce = alignmentForce;
    }

    private void OnEnable()
    {
        enabledTime = -0.6f;
    }
    void Update()
    {
        enabledTime += Time.deltaTime;
        float m = Mathf.Clamp01( enabledTime / 2f);
        alignmentForce = originalAlignmentForce * m * alignmentForceM;

        Vector3 relativePos = Vector3.zero;
        if (relativeTo.Length > 0)
        {
            foreach (Transform t in relativeTo)
            {
                relativePos += t.position;
            }
            relativePos /= relativeTo.Length;
        }
        //
        Vector3 diff = (relativePos - transform.position).WithY(0);
        rigidbody.AddForce(diff * alignmentForce, ForceMode.Impulse);
    }
}
