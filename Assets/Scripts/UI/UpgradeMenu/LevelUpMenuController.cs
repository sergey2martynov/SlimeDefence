using System.Collections.Generic;
using UI.UpgradeMenu;
using UnityEngine;

public class LevelUpMenuController : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<UpgradeTile> _upgradeTiles;
    [SerializeField] private ProgressController _progressController;
    [SerializeField] private LevelUpMenuDisabler _levelUpMenuDisabler;

    private void Start()
    {
        _progressController.PlayerLeveledUp += OnPlayerGetNewLevel;
    }
    
    private void OnDestroy()
    {
        _progressController.PlayerLeveledUp -= OnPlayerGetNewLevel;
    }

    private void OnPlayerGetNewLevel()
    {
        var upgrades = _upgradeManager.GetNewLevelUpgrades();

        for (int i = 0; i < _upgradeTiles.Count; i++)
        {
            _upgradeTiles[i].Initialize(upgrades[i], _levelUpMenuDisabler);
        }
        
        _levelUpMenuDisabler.LevelUpMenuDisable(true);
    }
}
