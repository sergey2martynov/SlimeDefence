using TMPro;
using UnityEngine;

namespace Core.Character.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ProgressController _progressController;
        [SerializeField] private Health _health;
        [SerializeField] private TextMeshProUGUI _levelText;

        private int _currentLevel;

        public int CurrentLevel => _currentLevel;

        public Health Health => _health;

        public ProgressController ProgressController => _progressController;

        private void Start()
        {
            _currentLevel = 1;
            _levelText.text = _currentLevel.ToString();
        }
        
        public void AddedPlayerLevel()
        {
            _currentLevel++;
            _levelText.text = _currentLevel.ToString();
        }
    }
}
