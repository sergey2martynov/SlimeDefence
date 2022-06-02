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
        private bool _isRemovedPositionY;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Xmove = Animator.StringToHash("Xmove");
        private static readonly int Zmove = Animator.StringToHash("Zmove");

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
                _animator.SetFloat(Xmove, _direction.x);
                _animator.SetFloat(Zmove, _direction.z);
                _animator.SetFloat(Speed, _direction.magnitude);
            }

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