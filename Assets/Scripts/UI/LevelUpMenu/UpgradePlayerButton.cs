using Core.Character.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelUpMenu
{
    public class UpgradePlayerButton : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Button _button;
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private LevelUpMenuDisabler _levelUpMenuDisabler;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _playerController.Upgrade();
            _pauseController.Pause(false);
            _levelUpMenuDisabler.LevelUpMenuDisable(false);
        }
    }
}
