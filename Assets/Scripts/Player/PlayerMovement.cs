using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Joystick _joystick;
    private Rigidbody _rigidbody;
    
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    private float _trueSpeed;
    private float _speedCounter;
    private float _timeToChangeSpeed = 0.5f;

    private void Start()
    {
        _trueSpeed = speed;
        _joystick = FindObjectOfType<Joystick>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * speed,_rigidbody.velocity.y,_joystick.Vertical * speed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            playerAnimator.SetBool("walk",true);
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _speedCounter += Time.fixedDeltaTime;
            if (_speedCounter >= _timeToChangeSpeed)
            {
                speed += Time.fixedDeltaTime;
                if (speed >= maxSpeed)
                {
                    playerAnimator.SetBool("run",true);
                    speed = maxSpeed;
                }
            }
        }
        else
        {
            _speedCounter = 0;
            speed = _trueSpeed;
            playerAnimator.SetBool("run",false);
            playerAnimator.SetBool("walk",false);
        }
    }
}
