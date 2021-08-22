using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AnticipationMovementState : CharacterMovementState
{
    public float duration = 1.4f;
    public CharacterMovementState nextState;
    //
    public override void RunState(CharacterPhysics thisCharacter, float time)
    {
        base.RunState(thisCharacter, time);
        // 
        if (time > duration)
        {
            thisCharacter.PlayMovementState(nextState);
        }
    }
}
