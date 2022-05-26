using UnityEngine;
using UnityEngine.UI;

namespace Upgrade.UI
{
    public class UpgradeTile : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;
        [SerializeField] private Image _icon;
        private IUpgradable _iUpgradable;
        private LevelUpMenuDisabler _disabler;

        public void Initialize(IUpgradable iUpgradable, LevelUpMenuDisabler disabler)
        {
            UpgradeParametersBase upgradeParameters = iUpgradable.GetUpgradeParameters();
            
            //_name.text = upgradeParameters.Name;
            _description.text = upgradeParameters.Description;
            //_icon.sprite = upgradeParameters.Icon.sprite;
            _button.onClick.AddListener(()=>
            {
                iUpgradable.Upgrade();
                disabler.LevelUpMenuDisable(false);
            });
        }

        public void RemoveListener()
        {
            _button.onClick.RemoveAllListeners();;
        }
    }
}