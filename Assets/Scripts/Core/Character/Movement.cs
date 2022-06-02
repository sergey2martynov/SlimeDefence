using System;
using Core.Character;
using DG.Tweening;
using StaticData;
using UnityEngine;
using Upgrade;

namespace CodeBase.Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : Upgradable
    {
        [SerializeField] private float _speed;
        [SerializeField] private SpeedLevels _speedLevels;
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private Animator _animator;

        private CharacterController _controller;
        private Vector3 _direction;
        private Vector3 _lookDirection;

        private bool _isRemovedPositionY;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Xmove = Animator.StringToHash("Xmove");
        private static readonly int Zmove = Animator.StringToHash("Zmove");

        public Vector3 Direction => _direction;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();

            if (_characterType == CharacterType.Player)
            {
                MaxLevel = _speedLevels.GetMaxNumberOfLevel();
                _speed = _speedLevels.GetSpeedParameters(_currentLevel).Amount;
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _controller.Move(_direction * Time.deltaTime * _speed);

            if (_animator != null)
            {
                var vector = new Vector2(_direction.x,  _direction.z);
                var angle = transform.localRotation.y * Mathf.Deg2Rad;
                var rotatedVector = new Vector2(vector.x * Mathf.Cos(angle) - vector.y * Mathf.Sin(angle),
                    vector.x * Mathf.Sin(angle) + vector.y * Mathf.Cos(angle));
                
                _animator.SetFloat(Xmove, rotatedVector.x);
                _animator.SetFloat(Zmove, rotatedVector.y);
                _animator.SetFloat(Speed, _direction.magnitude);
            }

            if (transform.position.y > 1f && !_isRemovedPositionY)
            {
                ReturnPositionY();
            }

            if (_direction.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(_lookDirection);
            }
        }

        public override void Upgrade()
        {
            _currentLevel++;
            _speed = _speedLevels.GetSpeedParameters(_currentLevel).Amount;
        }

        public override UpgradeParametersBase GetUpgradeParameters()
        {
            return _speedLevels.GetSpeedParameters(_currentLevel + 1);
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
        }

        public void SetLookDirection(Vector3 lookDirection, float rate = 0.01f)
        {
            if (Mathf.Approximately(rate, 0.01f))
            {
                _lookDirection = lookDirection;
            }
            else
                DOTween.To(() => _lookDirection, x => _lookDirection = x, lookDirection, rate);
        }

        private void ReturnPositionY()
        {
            _isRemovedPositionY = true;
            transform.DOMoveY(0f, 0.5f).OnComplete(() => _isRemovedPositionY = false);
        }
    }
}