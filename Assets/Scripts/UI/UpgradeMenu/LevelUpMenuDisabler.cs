using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Upgrade.UI;

public class LevelUpMenuDisabler : MonoBehaviour
{
    [SerializeField] private GameObject _levelUpMenu;
    
    [SerializeField] private List<UpgradeTile> _upgradeTiles;

    public void LevelUpMenuDisable(bool isActive)
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
