using UnityEngine;
using UpgradePlayer;

namespace Core.Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ProgressController _progressController;
        [SerializeField] private Health _health;

        private int _currentLevel;

        public int CurrentLevel => _currentLevel;

        public Health Health => _health;

        private PlayerParameters _currentPlayerParameters;

        public ProgressController ProgressController => _progressController;

        private void Start()
        {
            _currentLevel = 1;
            
            
        }
        
        public void AddedPlayerLevel()
        {
            _currentLevel++;
        }

    }
}
