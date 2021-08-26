using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionImpactSound : MonoBehaviour
{
    [SerializeField] private ImpactSound _impactSound;
    private CharacterPhysics _character;

    private void Start()
    {
        _character = GetComponentInParent<CharacterPhysics>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        CharacterPhysics otherCharacter = collision.gameObject.GetComponentInParent<CharacterPhysics>();
        //
        if (_character == null || otherCharacter == null || _character != otherCharacter)
        {
            _impactSound.Play(collision.contacts[0].point, collision.relativeVelocity.magnitude);
        }
    }
}
