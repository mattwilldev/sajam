using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJoint
{
    public Rigidbody rigidbody;
    public ConfigurableJoint joint;
    public JointDrive drive;
    public bool isArm;
    public bool isLeg; 
    public bool isLeft;
    public bool isRight;
    public bool isSpine;
    public bool isHead;
    public ConfigurableJoint previousJoint;
    public ConfigurableJoint nextJoint;
    public bool isDamaged;
}

public class CharacterPhysics : MonoBehaviour
{
    public Animator animator;  // ******* SO MOVEMENT STATES CAN ACCESS THE ANIMATOR ****
    //
    private CharacterMovementState _currentMovementState;
    private float _currentMovementStateTimer = 0;
    //
    private List<CharacterJoint> _characterJoints = new List<CharacterJoint>(); 
    //
    public float strengthM = 1; // ******* EVERY FRAME MULTIPLE JOINT STRENGTHS BY THIS VALUE ****
    public float armsStrengthM = 1;
    public float legsStrengthM = 1;
    public float spineStrengthM = 1;
    public float headStrengthM = 1;
    public float drunkStrengthM = 1;
    //
    //
    // ************* ALL THE EXTRA COMPONENTS MAKING THIS CHARACTER WORK *****
    //
    private HoldUpright  _holdUpright ;
    private float _holdUprightForce = 10;
    private FaceTarget _faceTarget;
    private float _faceTargetForce = 10;
    private MaintainHeight _maintainHeight;
    private float _maintainHeightHeight = 1;
    private MaintainVerticalAlignment _spineAlignment; 
    private ForwardMovementForce _movementForce; 
    private LimitVelocityBasedOnDirection _limitVelocity;
    private AttachToBox[] _attachToBoxes;
    private bool _canAttachToBoxes = true;
    //
    // ************* GETTING KNOCKED OUT AND BACK UP *****
    //
    public bool IsKnockedOut
    {
        get { return knockedOutTime > 0; }
    }
    public bool IsStaggered
    {
        get { return drunkDipTime > 0; }
    }
    private float knockedOutTime = 0; 
    private float drunkDipTime = 0; 
    //
    void Awake()
    {
        ConfigurableJoint[] joints = GetComponentsInChildren<ConfigurableJoint>();
        foreach (ConfigurableJoint configurableJoint in joints)
        {
            CharacterJoint newJoint = new CharacterJoint();
            newJoint.joint = configurableJoint;
            newJoint.drive = configurableJoint.angularXDrive;
            newJoint.previousJoint = configurableJoint.connectedBody.GetComponent<ConfigurableJoint>();
            newJoint.isArm = configurableJoint.name.ToLower().Contains("shoulder") || configurableJoint.name.ToLower().Contains("arm") || configurableJoint.name.ToLower().Contains("clavicle");
            newJoint.isLeg = configurableJoint.name.ToLower().Contains("thigh") || configurableJoint.name.ToLower().Contains("leg") || configurableJoint.name.ToLower().Contains("foot");
            newJoint.isLeft = configurableJoint.name.ToLower().Contains("left");
            newJoint.isRight = configurableJoint.name.ToLower().Contains("right");
            newJoint.isSpine = configurableJoint.name.ToLower().Contains("spine");
            newJoint.isHead = configurableJoint.name.ToLower().Contains("neck") || configurableJoint.name.ToLower().Contains("head");
            newJoint.rigidbody = configurableJoint.GetComponent<Rigidbody>();
            //
            _characterJoints.Add(newJoint);
        }

        foreach (CharacterJoint j in _characterJoints)
        {
            //******************************** CALCULATE THE JOINT CHAIN ... SO POTENTIALLY BEING HIT ON THE SHOULDER MAKES THE WHOLE ARM LIMP ***
            if (j.previousJoint != null)
            {
               for (int i = 0 ; i < _characterJoints.Count; i++)
                {
                    if (_characterJoints[i].joint == j.previousJoint)
                    {
                        _characterJoints[i].nextJoint = j.joint;
                    }
                }
            }
        }
        //
        // **************  GET THE OTHER MOVEMENT COMPONENTS **
        //
        _holdUpright = GetComponentInChildren<HoldUpright>();
        _holdUprightForce = _holdUpright.force;
        _faceTarget = GetComponentInChildren<FaceTarget>();
        _faceTargetForce = _faceTarget.force;
        _maintainHeight = GetComponentInChildren<MaintainHeight>();
        _maintainHeightHeight = _maintainHeight.height;
        _spineAlignment = GetComponentInChildren<MaintainVerticalAlignment>(); 
        _movementForce = GetComponentInChildren<ForwardMovementForce>();
        _limitVelocity = GetComponentInChildren<LimitVelocityBasedOnDirection>();
        _attachToBoxes = GetComponentsInChildren<AttachToBox>();
        // 
    }

    //
    void FixedUpdate()
    {
        // 
        // *********  TIMERS ***********
        // 
        //
        if (knockedOutTime > 0)
        { 
            // ********************  IS KNOCKED OUT *********
            knockedOutTime -= Time.deltaTime; 
            //
            if (knockedOutTime <= 0)
            {
                GetUp();
            }
        }
        else
        {
            // ********************  NOT KNOCKED OUT *********
            if (drunkDipTime > 0)
            { 
                drunkDipTime -= Time.deltaTime;
                if (drunkDipTime <= 0)
                {
                    SetMovementComponents(true);
                }
            }
            else
            { 
                drunkStrengthM = Mathf.Lerp(drunkStrengthM, 1, Time.deltaTime);
            }
        }
        // 
        // *********  ACTUALLY ADJUST JOINTS  ***********
        //
        foreach (CharacterJoint j in _characterJoints)
        {
            float m = strengthM * (j.isArm ? armsStrengthM : 1) * (j.isLeg ? legsStrengthM : 1) * (j.isSpine ? spineStrengthM : 1) * (j.isHead ? headStrengthM : 1) * (j.isHead || j.isSpine || j.isArm ? drunkStrengthM : 1);
            JointDrive drive = j.drive;
            drive.positionSpring *= m;
            drive.maximumForce *= m;
            drive.positionDamper *= m;
            j.joint.angularXDrive = j.joint.angularYZDrive = drive;
        }
        //
        if (_currentMovementState != null)
        {
            _currentMovementStateTimer += Time.deltaTime;
            _currentMovementState.RunState(this, _currentMovementStateTimer);
        }
    }

    //
    // **********************************************   MAKING THIS CHARACTER WORK  ***************************
    //
    public void Stagger(float time)
    {
         
        drunkDipTime = time;
        drunkStrengthM = 0.06f;
        SetMovementComponents(false);
    }
    //
    public void KnockOut(float time)
    {
 
        knockedOutTime = Mathf.Max(time,knockedOutTime);
        strengthM = 0.01f;
        //
        SetMovementComponents(false);
        GetTorso().drag = 0;
    }
    public void GetUp( )
    {
        strengthM = 1;
        knockedOutTime = 0;
        SetMovementComponents(true);
    }
    // ****************************   PLAY MOVEMENT STATE **************************
    //
    public void PlayMovementState(CharacterMovementState movementState)
    {
        if (movementState != _currentMovementState)
        {
            if (_currentMovementState != null)
            {
                _currentMovementState.ExitState(this);
            }
            Debug.Log("Start movement state " + movementState.name);
            movementState.StartState(this);
            _currentMovementState = movementState;
            _currentMovementStateTimer = 0;
        }
    }
    //
    //
    public void SetMaintainHeightComponents(bool enableComponent)
    { 
        _maintainHeight.enabled = enableComponent; 
    }

    public void SetMovementComponents(bool enableComponent)
    {
        //  
        _limitVelocity.enabled = enableComponent;        
        //
        _faceTarget.enabled = enableComponent;
        //
        _maintainHeight.enabled = enableComponent;
        //
        _holdUpright.enabled = enableComponent;
        //
        _spineAlignment.enabled = enableComponent;
        //
        foreach (AttachToBox aB in _attachToBoxes)
        {
            aB.enabled = enableComponent && _canAttachToBoxes;
        }
    }
    public void SetHoldUprightForce(float m)
    {
        _holdUpright.force = _holdUprightForce * m;
    }
    public void SetFacingForceM(float m)
    {
        _faceTarget.force = _faceTargetForce * m;
    }
    public void SetLimitVelocitiesM(float m)
    {
        _limitVelocity.limitVelocityM = m; 
    }
    public void SetExtraSpineDrag(float drag)
    {
        _limitVelocity.extraSpineDrag = drag; 
    }
    public void SetSpineAlignmentForceM(float m)
    {
        _spineAlignment.alignmentForceM = m;
        if (m == 0)
        {
            _spineAlignment.enabled = false;
        }
        else
        {
            _spineAlignment.enabled = true;
        }
    }
    public void SetLegJointForces(float m)
    {
        legsStrengthM = m;
    }
    public void SetSpineJointForces(float m)
    {
        spineStrengthM = m;
    }
    public void SetHeadJointForces(float m)
    {
        headStrengthM = m;
    }
    public void SetArmsJointForces(float m)
    {
        armsStrengthM = m;
    }
    public void SetMaintainHeightOffset(float offset)
    {
        _maintainHeight.height = _maintainHeightHeight + offset;
    }
    public void SetMovementForce(float force)
    {
        _movementForce.forwardForce = force;
    }
    //
    public void AddForceToHead(Vector3 force)
    {
        foreach (CharacterJoint j in _characterJoints)
        {
            if (j.isHead)
            {
                j.rigidbody.AddForce(force, ForceMode.Impulse);
            }
        }
    }
    public void AddForceToLegs(Vector3 force)
    {
        foreach (CharacterJoint j in _characterJoints)
        {
            if (j.isLeg)
            {
                j.rigidbody.AddForce(force, ForceMode.Impulse);
            }
        }
    }
    public void AddForceToSpine(Vector3 force)
    {
        if (!IsKnockedOut && !IsStaggered)
        {
            foreach (CharacterJoint j in _characterJoints)
            {
                if (j.isSpine)
                {
                    j.rigidbody.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }
    public void AddDirectionalForceToSpine(Vector3 force, Vector3 offset)
    {
        if (!IsKnockedOut && !IsStaggered)
        {
            foreach (CharacterJoint j in _characterJoints)
            {
                if (j.isSpine)
                {
                    j.rigidbody.AddForceAtPosition(force, j.rigidbody.transform.TransformPoint(offset), ForceMode.Impulse);
                }
            }
        }
    }
    public void AddForwardForceToHead(float force)
    {
        foreach (CharacterJoint j in _characterJoints)
        {
            if (j.isHead)
            {
                j.rigidbody.AddForce(j.rigidbody.transform.forward.WithY(0).normalized * force, ForceMode.Impulse);
            }
        }
    }
    public void AddForwardForceToSpine(float force)
    {
        foreach (CharacterJoint j in _characterJoints)
        {
            if (j.isSpine)
            {
                j.rigidbody.AddForce(j.rigidbody.transform.forward.WithY(0).normalized * force, ForceMode.Impulse);
            }
        }
    }
    public void AddForwardForceToLeftLeg(float force, float upForce)
    {
        if (!IsKnockedOut && !IsStaggered)
        {
            foreach (CharacterJoint j in _characterJoints)
            {
                if (j.isLeft && j.isLeg && j.nextJoint == null)
                {
                    j.rigidbody.AddForce(_faceTarget.transform.forward.WithY(0).normalized * force + Vector3.up * upForce, ForceMode.Impulse);
                }
            }
        }
    }
    public void AddForwardForceToRightLeg(float force, float upForce)
    {
        if (!IsKnockedOut && !IsStaggered)
        {
            foreach (CharacterJoint j in _characterJoints)
            {
                if (j.isRight && j.isLeg && j.nextJoint == null)
                {
                    j.rigidbody.AddForce(_faceTarget.transform.forward.WithY(0).normalized * force + Vector3.up * upForce, ForceMode.Impulse);
                }
            }
        }
    }
    public Rigidbody GetTorso()
    {
        foreach (CharacterJoint j in _characterJoints)
        {
            if (j.isSpine)
            {
                return j.rigidbody;
            }
        }
        return null;
    }
    public Rigidbody GetLeftHand()
    {
        foreach (CharacterJoint j in _characterJoints)
        {
            if (j.isArm && j.isLeft && j.nextJoint == null)
            {
                return j.rigidbody;
            }
        }
        return null;
    }
    public Rigidbody GetRightHand()
    {
        foreach (CharacterJoint j in _characterJoints)
        {
            if (j.isArm && j.isRight && j.nextJoint == null)
            {
                return j.rigidbody;
            }
        }
        return null;
    }
    public void SetCanAttachToBoxes(bool canAttach)
    {
        _canAttachToBoxes = canAttach;
        //
        foreach (AttachToBox aB in _attachToBoxes)
        {
            aB.enabled = !IsKnockedOut && !IsStaggered && _canAttachToBoxes;
        }
    }
}
