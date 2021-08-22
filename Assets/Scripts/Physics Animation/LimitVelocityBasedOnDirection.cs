using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitVelocityBasedOnDirection : MonoBehaviour
{
    public float limitVelocityM = 1;
    public float extraSpineDrag = 0;
    //
    [SerializeField] private float _drag = 6;
    
    [SerializeField] private float _zeroDragDOTThreshold = 0.4f;
    [SerializeField] private float _maxDragDOTThreshold = 0.1f;
    //
    [SerializeField] private float _differenceForce = 0;
    //
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float dot = Vector3.Dot(_rigidbody.velocity.normalized, transform.forward);
        //
        if (dot > _zeroDragDOTThreshold)
        {
            _rigidbody.drag = 0 + extraSpineDrag;
            //
        }
        else if (dot > _maxDragDOTThreshold)
        {
            _rigidbody.drag = Mathf.Lerp(0, _drag * limitVelocityM, (dot - _maxDragDOTThreshold) / (_zeroDragDOTThreshold - _maxDragDOTThreshold)) + extraSpineDrag;
        }
        else
        {
            _rigidbody.drag = _drag * limitVelocityM + extraSpineDrag;
        }
        if (_differenceForce > 0 && limitVelocityM > 0)
        {
            Vector3 desiredDirection = transform.forward.WithY(0).normalized;
            Vector3 currentDirection = _rigidbody.velocity;
            _rigidbody.velocity =  Vector3.Lerp(currentDirection, desiredDirection.WithMagnitude(currentDirection.magnitude), _differenceForce * Time.deltaTime * limitVelocityM);
        }
    }
}
