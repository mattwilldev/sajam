using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    
    public bool IsAlive
    {
        get { return _health > 0; }
    }

    private CharacterPhysics _characterPhysics;

    //
    private void Start()
    {
        _characterPhysics = GetComponent<CharacterPhysics>();
    }

    public void Damage(int damage, GameObject damager, Collision collision, Vector3 force)
    {
        _health -= damage; 
        if (IsAlive)
        {
            _characterPhysics.Stagger(2f);
            _characterPhysics.AddForceToSpine(force * 0.1f);
           // collision.rigidbody.AddForce(force * 0.2f, ForceMode.Impulse);
        }
        else if (!_characterPhysics.IsKnockedOut)
        {
            _characterPhysics.KnockOut(Mathf.Infinity);
        }
    }
}
