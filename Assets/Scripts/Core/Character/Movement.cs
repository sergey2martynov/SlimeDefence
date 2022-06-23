using System;
using CodeBase.Core.Character.Player;
using Core.Character;
using DG.Tweening;
using StaticData;
using UnityEngine;
using Upgrade;

namespace CodeBase.Core.Character
{
    public class Movement : Upgradable
    {
        [SerializeField] private float _speed;
        [SerializeField] private SpeedLevels _speedLevels;
        [SerializeField] private CharacterType _characterType;

        private CharacterController _controller;
        private Vector3 _direction;
        private Vector3 _lookDirection;

        private bool _isRemovedPositionY;

        public Vector3 Direction => _direction;

        private void Awake()
        {
            if (_characterType == CharacterType.Player)
            {
                MaxLevel = _speedLevels.GetMaxNumberOfLevel();
                _speed = _speedLevels.GetSpeedParameters(_currentLevel).Amount;
            }
        }

        private void Update()
        {
            if (_characterType == CharacterType.Player)
                transform.rotation = Quaternion.LookRotation(_lookDirection);
        }

        public void SetDirection(Vector3 direction)
        {
            direction.y = 0;
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
    }
}