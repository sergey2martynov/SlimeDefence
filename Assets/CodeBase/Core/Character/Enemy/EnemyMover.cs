using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterController _targetObject;
    [SerializeField] private Rigidbody _rigidbody;

    private float _speed;
    private Vector3 _target;

    private void Start()

    {
        _speed = _characterController.GetSpeed();
    }

    private void FixedUpdate()
    {       
        MoveEnemy(_target, _speed);
    }

    private void MoveEnemy(Vector3 target, float speed)
    {
        _target = _targetObject.transform.position;
        gameObject.transform.position = Vector3.MoveTowards(transform.position, target, speed);
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);        
    }
}
