using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterMovementState _walkingState;
    [SerializeField] private CharacterMovementState _idleState;
    [SerializeField] private CharacterMovementState _jumpingState;
    [SerializeField] private CharacterMovementState _hurtState;
    [SerializeField] private CharacterMovementState _highFiveState;
    //
    private CharacterMovementState _currentMovementState;
    //
    [SerializeField] private FaceTarget _faceTarget;
    //
    private CharacterPhysics _characterPhysics;
    //
    public KeyCode keyCodeUp = KeyCode.UpArrow;
    public KeyCode keyCodeDown = KeyCode.DownArrow;
    public KeyCode keyCodeLeft = KeyCode.LeftArrow;
    public KeyCode keyCodeRight = KeyCode.RightArrow;
    public KeyCode keyCodeJump = KeyCode.RightControl;
    public KeyCode keyCodeHighFive = KeyCode.RightShift;
    //
    private float _inputDelay = 0;
    //
    void Start()
    {
        _characterPhysics = GetComponentInChildren<CharacterPhysics>();
    }


    void Update()
    {
        Vector3 direction = Vector3.zero;
        //
        if (_inputDelay <= 0)
        {
            if (Input.GetKey(keyCodeUp))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(keyCodeDown))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(keyCodeLeft))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(keyCodeRight))
            {
                direction += Vector3.right;
            }
            if (Input.GetKeyDown(keyCodeHighFive))
            {
                SetMovementState(_highFiveState);
                _inputDelay = 0.66f;
                direction = Vector3.zero;
            }
            if (Input.GetKeyDown(keyCodeJump))
            {
                SetMovementState(_jumpingState);
                _inputDelay = 1.5f;
                direction = Vector3.zero;
            }
        }
        else
        {
            _inputDelay -= Time.deltaTime;
        }
        //
        if (!_characterPhysics.IsStaggered && !_characterPhysics.IsKnockedOut)
        {
            if (direction != Vector3.zero)
            {
                // ***************  TRY WALK AROUND ******************
                //
                direction.Normalize();
                _faceTarget.direction = Vector3.RotateTowards(_faceTarget.direction, direction, Time.deltaTime * 6, Time.deltaTime * 6);
                if (_currentMovementState != _walkingState)
                {
                    SetMovementState(_walkingState);
                }
            }
            else
            {
                if (_currentMovementState != _idleState && _inputDelay <= 0)
                {
                    SetMovementState(_idleState);
                }
            }
        }
        else
        {
            if (_currentMovementState != _hurtState)
            {
                SetMovementState(_hurtState);
            }
        }
        // 
    }
    private void SetMovementState(CharacterMovementState newMovementState)
    {
        _characterPhysics.PlayMovementState(newMovementState);
        _currentMovementState = newMovementState;
    }
}
