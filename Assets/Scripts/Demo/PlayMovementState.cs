using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct MovementStateKeyPair
{
    public KeyCode keyCode;
    public CharacterMovementState movementState;

}
public class PlayMovementState : MonoBehaviour
{
    public MovementStateKeyPair[] movementStateKeyPair;
    //
    private CharacterPhysics characterPhysics;

    private void Awake()
    {
        characterPhysics = GetComponent<CharacterPhysics>();
    }

    void Update()
    {
        foreach (MovementStateKeyPair mKP in movementStateKeyPair)
        {
            if (Input.GetKeyDown(mKP.keyCode))
            {
                characterPhysics.PlayMovementState(mKP.movementState);

            }
            
        }
    }
}
