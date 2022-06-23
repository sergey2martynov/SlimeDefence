using System.Collections.Generic;
using UI.UpgradeMenu;
using UnityEngine;

public class NewWeaponMenu : MonoBehaviour
{
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private UpgradeTile _upgradeTile;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private List<GameObject> _weaponModels;
    [SerializeField] private List<ParticleSystem> _confetti;
    [SerializeField] private EnemyHealthBarFiller _healthBar;

    private int count;

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
        if (_upgradeManager.NewWeapons.Count == 0)
            return;

        _weaponModels[count].SetActive(true);
        count++;

        var disableDelegate = new DisableDelegate(DisableMenu);

        var upgradeTile = _upgradeManager.GetNewWeapon();

        _upgradeTile.Initialize(upgradeTile, disableDelegate);

        foreach (var t in _confetti)
        {
            t.gameObject.SetActive(true);
            t.Play();
        }
        
        _healthBar.gameObject.SetActive(false);

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
            if (count == 0)
                _weaponModels[count].SetActive(false);
            else
                _weaponModels[count - 1].SetActive(false);
            
            foreach (var t in _confetti)
            {
                t.gameObject.SetActive(false);
                t.Play();
            }
            
            _healthBar.gameObject.SetActive(true);
        }
        
        gameObject.SetActive(isActive);
    }
}