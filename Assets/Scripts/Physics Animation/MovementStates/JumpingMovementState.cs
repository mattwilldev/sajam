using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JumpingMovementState : CharacterMovementState
{
    public float jumpForce = 40;
    public float jumpSpinForce = 10;
    public float jumpSpinTime = 0.33f;
    //
    public float duration = 1.4f;
    public CharacterMovementState nextState;
    //
    private Vector3 jumpDirection;
    //
    public override void StartState(CharacterPhysics thisCharacter)
    {
        base.StartState(thisCharacter);
        //
        jumpDirection = thisCharacter.GetTorso().transform.forward.WithY(0).normalized;
        //
        if (!thisCharacter.IsKnockedOut)
        {
            thisCharacter.AddForwardForceToSpine(jumpForce * 0.5f);
            thisCharacter.AddForceToSpine(Vector3.up * jumpForce * 0.5f);
            thisCharacter.AddForwardForceToHead(jumpForce * 0.3f);
        }
    }
    public override void RunState(CharacterPhysics thisCharacter, float time)
    {
        base.RunState(thisCharacter, time);
        //
        if (!thisCharacter.IsKnockedOut)
        {
            if (time < jumpSpinTime)
            {
                thisCharacter.AddForceToHead(Vector3.up * -jumpSpinForce * Time.deltaTime);
                thisCharacter.AddForceToLegs(Vector3.up * jumpSpinForce * 0.1f * Time.deltaTime);
            }
            else
            {
                thisCharacter.AddDirectionalForceToSpine(Vector3.down * jumpForce * 2 * Time.deltaTime, Vector3.forward);
                //
                thisCharacter.AddDirectionalForceToSpine(jumpDirection * jumpForce * 1f * Time.deltaTime, Vector3.up);  //  **** PULL THE TORSO STRAIGHT ***
                thisCharacter.AddDirectionalForceToSpine(jumpDirection * jumpForce * -1f * Time.deltaTime, Vector3.down);
            }
        }
       
        if (time > duration)
        {
            thisCharacter.PlayMovementState(nextState);
        }
    }
}
