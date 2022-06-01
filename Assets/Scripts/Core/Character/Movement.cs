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

        private CharacterController _controller;
        private Vector3 _direction;
        private bool _isRemovedPositionY;

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

            if (transform.position.y > 1f && !_isRemovedPositionY)
            {
                ReturnPositionY();
            }

            if (_direction.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(_direction);
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

        private void ReturnPositionY()
        {
            _isRemovedPositionY = true;
            transform.DOMoveY(0f, 0.5f).OnComplete(() => _isRemovedPositionY = false);
        }
    }
}