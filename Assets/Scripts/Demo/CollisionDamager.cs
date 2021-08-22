using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamager : MonoBehaviour
{
    [SerializeField] private float minimumSpeed = 3;
    [SerializeField] private int dealsDamage = 1;
    [SerializeField] private string ignoreTag = "DontAttachTo";
    private Vector3 currentVelocity;
    private Rigidbody _rigidbody;
    private float lastDamagedTime = 0;
    private CharacterHealth _thisHealth;
    //
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _thisHealth = GetComponentInParent<CharacterHealth>();
    }
    private void FixedUpdate()
    {
        currentVelocity = _rigidbody.velocity;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody != null && Time.time - lastDamagedTime > 0.3f && currentVelocity.magnitude > minimumSpeed &&  other.relativeVelocity.magnitude >= minimumSpeed && !other.gameObject.CompareTag(ignoreTag))
        {
            CharacterHealth health = other.gameObject.GetComponentInParent<CharacterHealth>();
            if (health != null && health != _thisHealth)
            {
                lastDamagedTime = Time.time;
                // **** HAS HIT A CHARACTER ***
                health.Damage(dealsDamage, gameObject, other, currentVelocity);
            }
        }
    }
}