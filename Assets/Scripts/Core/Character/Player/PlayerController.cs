using UnityEngine;
using UpgradePlayer;

namespace Core.Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ProgressController _progressController;


        private int _currentLevel;

        public int CurrentLevel => _currentLevel;

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
