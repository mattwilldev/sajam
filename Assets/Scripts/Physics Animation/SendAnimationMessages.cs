using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAnimationMessages : MonoBehaviour
{
    [SerializeField] private CharacterPhysics characterPhysics;

    public void KickLeftLegForward()
    {
        Debug.Log("KickLeftLegForward ");
        characterPhysics.AddForwardForceToLeftLeg(1.5f,2);
    }
    public void KickRightLegForward()
    {
        Debug.Log("KickRightLegForward ");
        characterPhysics.AddForwardForceToRightLeg(1.5f, 2);
    }
    public void KickLeftLegUp()
    {
        Debug.Log("KickLeftLegUp ");
        characterPhysics.AddForwardForceToLeftLeg(0, 3);
    }
    public void KickRightLegUp()
    {
        Debug.Log("KickRightLegUp ");
        characterPhysics.AddForwardForceToRightLeg(0, 3);
    }
    public void KickLeftLegBack()
    {
        Debug.Log("KickLeftLegBackward ");
        characterPhysics.AddForwardForceToLeftLeg(-0.9f, -1.5f);
        characterPhysics.AddForwardForceToRightLeg(0, 2.5f);
    }
    public void KickRightLegBack()
    {
        Debug.Log("KickRightLegBackward ");
        characterPhysics.AddForwardForceToRightLeg(-0.9f, -1.5f);
        characterPhysics.AddForwardForceToLeftLeg(0, 2.5f);
    }
    public void PushSpineUp()
    {
        Debug.Log("PushSpineUp ");
        characterPhysics.AddForceToSpine(Vector3.up * 8.5f);
    }
    public void PushSpineUpSlightly()
    {
        Debug.Log("PushSpineUp Slightly");
        characterPhysics.AddForceToSpine(Vector3.up * 2.5f);
    }
}
