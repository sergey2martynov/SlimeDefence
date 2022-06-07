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
        private bool _isCanMove = true;

        private bool _isRemovedPositionY;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Xmove = Animator.StringToHash("Xmove");
        private static readonly int Zmove = Animator.StringToHash("Zmove");

        public Vector3 Direction => _direction;
        public bool IsCanMove => _isCanMove;

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
            if (_isCanMove)
                _controller.Move(_direction * Time.deltaTime * _speed);

            if (_animator != null)
            {
                var angle = Vector3.Angle(_lookDirection, _direction) * Mathf.Deg2Rad;

                _animator.SetFloat(Xmove, Mathf.Sin(angle));
                _animator.SetFloat(Zmove, Mathf.Cos(angle));
                _animator.SetFloat(Speed, _direction.magnitude);
            }

            transform.rotation = Quaternion.LookRotation(_lookDirection);
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

        public void Push()
        {
            // _isCanMove = false;
            // transform.DOMove((transform.position - _direction * 2f) , 0.3f).OnComplete(() => _isCanMove = true);
        }
    }
}