using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFacing : MonoBehaviour {

    protected int facingDirection = 1;
    protected  float lastHopTime = -3;
   virtual  public bool BusyChangingSides { get { return Time.time - lastHopTime  <= 0.6f; } }

    virtual public int FacingDirection { get { return facingDirection; } }

  virtual  public void FaceDirection(Vector3 direction)
    {

    }

   
}
