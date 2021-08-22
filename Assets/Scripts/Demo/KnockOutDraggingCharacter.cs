using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOutDraggingCharacter : MonoBehaviour
{
    [SerializeField] private CharacterMovementState _limpState;
    private  UnityStandardAssets.Utility.DragRigidbody dragger;
    private Rigidbody _lastDraggedRagdollBody;

    void Start()
    {
        dragger = GetComponent<UnityStandardAssets.Utility.DragRigidbody>();
    }

     
    void Update()
    {
        if (dragger.DraggingBody != null)
        {
            CharacterPhysics c = dragger.DraggingBody.GetComponentInParent<CharacterPhysics>();
            if (c != null)
            {
                _lastDraggedRagdollBody = dragger.DraggingBody;
                c.KnockOut(1.5f);
                if (_limpState != null) c.PlayMovementState(_limpState);
            }
        }
        else
        {
            if (_lastDraggedRagdollBody != null)
            {
                _lastDraggedRagdollBody.drag = 0; // ***** RESET THE DRAG IN CASE IT WAS PICKED UP WHILE DRAG WAS ON IT ***
                _lastDraggedRagdollBody = null;
            }
        }
    }
}
