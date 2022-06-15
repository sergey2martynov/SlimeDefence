using UnityEngine;
using UnityEngine.UI;
using Upgrade;

namespace UI.UpgradeMenu
{
    public class UpgradeTile : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;
        [SerializeField] private Image _icon;
        private LevelUpMenuDisabler _disabler;

        public void Initialize(Upgradable iUpgradable, NewWeaponMenu.DisableDelegate disableDelegate)
        {
            UpgradeParametersBase upgradeParameters = iUpgradable.GetUpgradeParameters();
            
            _name.text = upgradeParameters.Name;
            //_description.text = upgradeParameters.Description;
            //_icon.sprite = upgradeParameters.Icon.sprite;
            _button.onClick.AddListener(()=>
            {
                iUpgradable.Upgrade();
                disableDelegate.Invoke(false);
            });
        }

        public void RemoveListener()
        {
            _button.onClick.RemoveAllListeners();;
        }
    }
}