using UI.UpgradeMenu;
using UnityEngine;

public class NewWeaponMenu : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private UpgradeTile _upgradeTile;
    [SerializeField] private TimeCounter _timeCounter;

    public delegate void DisableDelegate(bool isActive);
    
    private void Start()
    {
        _timeCounter.WeaponReceived += OnPlayerGetNewWeapon;
        DisableMenu(false);
        Time.timeScale = 0;
    }
    
    private void OnDestroy()
    {
        _timeCounter.WeaponReceived -= OnPlayerGetNewWeapon;
    }
    
    private void OnPlayerGetNewWeapon()
    {
        var disableDelegate = new DisableDelegate(DisableMenu);
        
        var upgradeTile = _upgradeManager.GetNewWeapon();
        
        _upgradeTile.Initialize(upgradeTile, disableDelegate);
        
        DisableMenu(true);
    }

    private void DisableMenu(bool isActive)
    {
        if (isActive)
            Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;
                _upgradeTile.RemoveListener();
        }
        
        gameObject.SetActive(isActive);
    }
}
