using UI.UpgradeMenu;
using UnityEngine;
using Upgrade;

public class LevelUpMenuController : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private UpgradeTile _upgradeTile;
    [SerializeField] private ProgressController _progressController;
    [SerializeField] private LevelUpMenuDisabler _levelUpMenuDisabler;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private int _rateForUpgradeWeapon = 2;
    
    private int _upgradeCount;

    private void Start()
    {
        _progressController.PlayerLeveledUp += OnPlayerGetNewLevel;
        _timeCounter.WeaponReceived += OnPlayerGetNewWeapon;
    }
    
    private void OnDestroy()
    {
        _progressController.PlayerLeveledUp -= OnPlayerGetNewLevel;
        _timeCounter.WeaponReceived -= OnPlayerGetNewWeapon;
    }

    private void OnPlayerGetNewLevel()
    {
        Upgradable upgradeTile;
        
        if (_upgradeCount < _rateForUpgradeWeapon)
        {
            upgradeTile = _upgradeManager.GetUpgradableStatesPlayer();
        }
        else
        {
            upgradeTile = _upgradeManager.GetUpgradableWeapon();
            _upgradeCount = -1;
        }
        
        _upgradeTile.Initialize(upgradeTile, _levelUpMenuDisabler);
        
        _levelUpMenuDisabler.LevelUpMenuDisable(true);

        _upgradeCount++;
    }
    
    private void OnPlayerGetNewWeapon()
    {
        var upgradeTile = _upgradeManager.GetNewWeapon();
        
        _upgradeTile.Initialize(upgradeTile, _levelUpMenuDisabler);
        
        _levelUpMenuDisabler.LevelUpMenuDisable(true);
    }
    
}
