using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HighFiveMovementState : CharacterMovementState
{
    public float highFivePullForce = 4; 
    //
    public float duration = 1.4f;
    public CharacterMovementState nextState;
    // 
    //
    public override void StartState(CharacterPhysics thisCharacter)
    {
        base.StartState(thisCharacter);
        // 
    }
    public override void RunState(CharacterPhysics thisCharacter, float time)
    {
        base.RunState(thisCharacter, time);
        // 
        if (!thisCharacter.IsKnockedOut)
        {
            Rigidbody highFiveHand = thisCharacter.GetLeftHand();
           
            HighFiveArm otherArm = HighFiveArm.GetNearestWithin(highFiveHand.transform.position, 2);
            if (otherArm != null )
            {
                highFiveHand.AddForce((otherArm.transform.position - highFiveHand.transform.position) * highFivePullForce * Time.deltaTime, ForceMode.Impulse);
            }
        } 
        if (time > duration)
        {
            thisCharacter.PlayMovementState(nextState);
        }
    }
}
