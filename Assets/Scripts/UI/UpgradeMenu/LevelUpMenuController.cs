using System.Collections.Generic;
using UI.UpgradeMenu;
using UnityEngine;
using Upgrade;

public class LevelUpMenuController : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private List<UpgradeTile> _upgradeTiles;
    [SerializeField] private ProgressController _progressController;
    [SerializeField] private GameObject _levelUpMenu;

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
        NewWeaponMenu.DisableDelegate disableDelegate = DisableMenu;
        
        var upgrades = _upgradeManager.GetNewLevelUpgrades();

        for (int i = 0; i < _upgradeTiles.Count; i++)
        {
            _upgradeTiles[i].Initialize(upgrades[i], disableDelegate);
        }

        DisableMenu(true);
    }

    private void DisableMenu(bool isActive)
    {
        if (isActive)
            Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;

            foreach (var tile in _upgradeTiles)
            {
                tile.RemoveListener();
            }
        }
        _levelUpMenu.SetActive(isActive);
    }
    
}
