using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachOnCollision : MonoBehaviour
{
    [SerializeField] private int dealsDamage = 0;
    [SerializeField] private float minimumSpeed = 5;
    [SerializeField] private string ignoreTag = "DontAttachTo"; // **** ARROWS DEMATERIALIZE QUICKLY IF THEY START ATTACHING TO THEMSELVES ****
    [SerializeField] private float jointForce = 40;
    [SerializeField] private bool onlyAttachIfDirectHit = true;
    [SerializeField] private float breakForce = 200;  // **** SET TO INFINITY FOR INDESTRUCTIBLE JOINT ****
    //
    [SerializeField] private float lifeTimeUnattached = 5;  // **** SET TO INFINITY FOR NO TIME OUT ****
    [SerializeField] private GameObject destroyUponDeath ;
    private Vector3 currentVelocity;
    private Rigidbody _rigidbody;

    private ConfigurableJoint joint;
    //
    private bool hasAttached = false;
    //
    
    //
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        lifeTimeUnattached += Random.value;
    }
    private void FixedUpdate()
    {
        if (!hasAttached)
        {
            currentVelocity = _rigidbody.velocity;
            //
            RunLife();
        }
        else
        {
            if (joint == null)
            {
                hasAttached = false;
            }
            else if (joint.connectedBody == null)
            {
                RunLife();
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!hasAttached)
        { 
            if (  other.relativeVelocity.magnitude >= minimumSpeed && !other.gameObject.CompareTag(ignoreTag) &&    (!onlyAttachIfDirectHit || Vector3.Dot(other.contacts[0].normal, currentVelocity.normalized) < -0.7f))
            {
                //
                hasAttached = true; 
                //
                joint = gameObject.AddComponent<ConfigurableJoint>();
                joint.connectedBody = other.rigidbody;
                //
                joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Locked;
                //
                JointDrive drive = joint.angularXDrive;
                drive.positionDamper = jointForce * 0.02f;
                drive.maximumForce = jointForce;
                drive.positionSpring = jointForce * 2;
                joint.angularXDrive = joint.angularYZDrive = drive;
                joint.enablePreprocessing = false;
                joint.breakForce = breakForce;
                //
                GetComponent<Collider>().enabled = false; 
                //
                if (dealsDamage > 0)
                {
                    CharacterHealth health = other.gameObject.GetComponentInParent<CharacterHealth>();
                    if (health != null)
                    {
                        // **** HAS HIT A CHARACTER ***
                        health.Damage(dealsDamage, gameObject, other, currentVelocity);
                    }
                }

                //
            }
        }
    }
    private void RunLife()
    {
        lifeTimeUnattached -= Time.deltaTime;
        //
        if (lifeTimeUnattached <= 0 && destroyUponDeath != null)
        {
            Destroy(destroyUponDeath); // ******* DESTROY THE PARENT OBJECT *****
        }
    }
}
