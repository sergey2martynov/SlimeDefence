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

        public int CurrentLevel => _currentLevel;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            
            if (_movementType == MovementType.PlayerMovement)
            {
                _speed = _speedLevels.GetSpeedParameters(_currentLevel).Amount;
            }
        }

        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            _controller.Move(_direction * Time.deltaTime * _speed);

            if (transform.position.y > 0f)
            {
                transform.DOMoveY(0f, 1f);
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
            return _speedLevels.GetSpeedParameters(_currentLevel+1);
        }
        

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
        }
    }
}