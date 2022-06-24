using Analytics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrade;

namespace UI.UpgradeMenu
{
    public class UpgradeTile : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Text _description;
        [SerializeField] private Image _icon;
        [SerializeField] private AudioSource _pressSound;
        private LevelUpMenuDisabler _disabler;

        public void Initialize(Upgradable iUpgradable, NewWeaponMenu.DisableDelegate disableDelegate)
        {

            UpgradeParametersBase upgradeParameters = iUpgradable.GetUpgradeParameters();
            
            _name.text = upgradeParameters.Name;
            //_description.text = upgradeParameters.Description;
            _icon.sprite = upgradeParameters.UprgradeIcon.sprite;
            _icon.rectTransform.sizeDelta = new Vector2(100, 100);
            _button.onClick.AddListener(()=>
            {
                _pressSound.Play();
                if (!iUpgradable.IsActive)
                {
                    EventSender.SendLevelStart();
                }
                
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