using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglyEyeFakeJoint : MonoBehaviour
{
    [SerializeField] private Transform _pupil;
    [SerializeField] private float _velocityFromForceM = 50;
    [SerializeField] private float _damping = 1.5f;
    //
    private float _rotationSpeed = 0; // **** CURRENT ROTATION SPEED AROUND Z ***
    // 
    private Vector3 _gravity ;  
    private Vector3 _currentGravity =Vector3.zero;
    private float _gravityLerp = 19;
    private Vector3 _lastPosition;
    private Vector3 _lastVelocity;

    void Start()
    {
        _gravity = Physics.gravity ;
        _lastPosition = transform.position;
    }

    
    void Update()
    {
        Vector3 velocity = (transform.position - _lastPosition) / Time.deltaTime;
        Vector3 acceleration = velocity - _lastVelocity;
        //
        _currentGravity =  Vector3.Lerp(_currentGravity, _gravity - acceleration, Time.deltaTime * _gravityLerp);
        //
        float direction = Vector3.Dot(_pupil.right, _currentGravity);
        //
        _rotationSpeed = (_rotationSpeed * (1 - Time.deltaTime * _damping) + direction * _currentGravity.magnitude * _velocityFromForceM * Time.deltaTime);
        // 
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime, Space.Self);
        //
        _lastVelocity = velocity;
        _lastPosition = transform.position;
    }
}
