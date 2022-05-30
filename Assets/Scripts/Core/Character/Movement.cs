using Core.Character;
using DG.Tweening;
using StaticData;
using UnityEngine;
using Upgrade;

namespace CodeBase.Core.Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour, IUpgradable
    {
        [SerializeField] private float _speed;
        [SerializeField] private SpeedLevels _speedLevels;
        [SerializeField] private MovementType _movementType;

        private CharacterController _controller;
        private Vector3 _direction;
        private int _currentLevel;
        private bool _isRemovedPositionY;

        public int CurrentLevel => _currentLevel;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();

            if (_movementType == MovementType.PlayerMovement)
            {
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

        public void Upgrade()
        {
            _currentLevel++;
            _speed = _speedLevels.GetSpeedParameters(_currentLevel).Amount;
        }

        public UpgradeParametersBase GetUpgradeParameters()
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