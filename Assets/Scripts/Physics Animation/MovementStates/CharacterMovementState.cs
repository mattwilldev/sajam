using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterMovementState : ScriptableObject
{ 
   
    public string fullBodyAnimationState = "Idle";
    //
    public float fullBodyJointFollowForceM = 1;
    public float legJointFollowForceM = 1;
    public float armsJointFollowForceM = 1;
    public float spineJointFollowForceM = 1;
    public float headJointFollowForceM = 1;
   
    public float facingDirectionM = 1;
    public float movementForwardForce = 0;
    public float holdUprightForceM = 1;
    public float spineAlignmentForceM = 1;
    public float limitVelocitiesM = 1;
    public float spineDrag = 0;
    public bool maintainHeights = true; 
    public float maintainHeightOffset = 0;
     
  //  public EffectSoundBank startSound;
  //  public EffectSoundBank vocalSound;

    virtual public void StartState(CharacterPhysics thisCharacter)
    {
        //
        if (fullBodyAnimationState != null && fullBodyAnimationState.Length > 0) thisCharacter.animator.Play(fullBodyAnimationState, 0);
        // 
        thisCharacter.SetLegJointForces(legJointFollowForceM * fullBodyJointFollowForceM);
        thisCharacter.SetSpineJointForces(spineJointFollowForceM * fullBodyJointFollowForceM);
        thisCharacter.SetHeadJointForces(headJointFollowForceM * fullBodyJointFollowForceM);
        thisCharacter.SetArmsJointForces(armsJointFollowForceM * fullBodyJointFollowForceM);
        //
        thisCharacter.SetHoldUprightForce(holdUprightForceM);
        thisCharacter.SetMaintainHeightOffset(maintainHeightOffset);
        thisCharacter.SetMovementForce(movementForwardForce);
        thisCharacter.SetFacingForceM(facingDirectionM);
        thisCharacter.SetSpineAlignmentForceM(spineAlignmentForceM);
        thisCharacter.SetLimitVelocitiesM(limitVelocitiesM);
        thisCharacter.SetExtraSpineDrag(spineDrag);
        //
        if (maintainHeights)
        {
            thisCharacter.SetMaintainHeightComponents(true);
        }
        else
        {
            thisCharacter.SetMaintainHeightComponents(false);
        }
       // startSound.Play();
    }
    virtual public void RunState(CharacterPhysics thisCharacter, float time)
    {
         
    }
    virtual public void ExitState(CharacterPhysics thisCharacter)
    {
        
    }
}
