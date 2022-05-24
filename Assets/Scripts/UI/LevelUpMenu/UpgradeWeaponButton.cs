using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelUpMenu
{
    public class UpgradeWeaponButton : MonoBehaviour
    {
        [SerializeField] private AbstractWeapon _weapon;
        [SerializeField] private Button _button;
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private LevelUpMenuDisabler _levelUpMenuDisabler;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _weapon.Upgrade();
            _pauseController.Pause(false);
            _levelUpMenuDisabler.LevelUpMenuDisable(false);
        }
    }
}