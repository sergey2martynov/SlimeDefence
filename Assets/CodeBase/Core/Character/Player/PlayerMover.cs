using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _fixedJoystick;


    private void Update()
    {
        _rigidbody.velocity = new Vector3(-_fixedJoystick.Horizontal * _characterController.GetSpeed(), 
            _rigidbody.velocity.y, -_fixedJoystick.Vertical * _characterController.GetSpeed());

        if (_fixedJoystick.Horizontal !=0 || _fixedJoystick.Vertical !=0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }
}
