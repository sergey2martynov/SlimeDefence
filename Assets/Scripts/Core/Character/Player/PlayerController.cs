using CodeBase.Core.Character;
using UnityEngine;
using UpgradePlayer;

namespace Core.Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ProgressController _progressController;
        [SerializeField] private Health _health;
        [SerializeField] private Movement _movement;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private PlayerLevels _playerLevels;

        private int _currentLevel;

        public int CurrentLevel => _currentLevel;

        private PlayerParameters _currentPlayerParameters;

        public ProgressController ProgressController => _progressController;

        private void Start()
        {
            SetCurrentStates(1);
        }

        public void Upgrade()
        {
            SetCurrentStates(_currentLevel);
        }

        public void AddedPlayerLevel()
        {
            _currentLevel++;
        }

        private void SetCurrentStates(int level)
        {
            _currentPlayerParameters = _playerLevels.GetPlayerParameters(level);
            _health.SetHealth(_currentPlayerParameters.Health);
            _movement.SetSpeed(_currentPlayerParameters.Speed);
            _capsuleCollider.radius = _currentPlayerParameters.PikUpRadius;
        }
    }
}
