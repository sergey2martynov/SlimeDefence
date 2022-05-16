﻿using System;
using UnityEngine;

namespace CodeBase.Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    
    public class Movement : MonoBehaviour
    {
        private CharacterController _controller;
        [SerializeField] private float _speed;
        private Vector3 _direction;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            _controller.Move(_direction * Time.deltaTime * _speed);
        
            if (_direction.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(_direction);
            }
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
        }
    }
}