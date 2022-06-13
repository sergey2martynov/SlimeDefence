using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrade;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Upgradable> _upgradableStatesPlayer;
    [SerializeField] private List<Upgradable> _upgradableWeapon;
    [SerializeField] private List<Upgradable> _newWeapons;

    private int _currentWave;

    public Upgradable GetUpgradableStatesPlayer()
    {
        return _upgradableStatesPlayer[Random.Range(0,_upgradableStatesPlayer.Count)];
    }

    public Upgradable GetUpgradableWeapon()
    {
        return _upgradableWeapon[Random.Range(0, _upgradableWeapon.Count)];
    }

    public Upgradable GetNewWeapon()
    {
        var weapon = _newWeapons[0];
        _newWeapons.Remove(_newWeapons[0]);
        _upgradableWeapon.Add(weapon);

        return weapon;
    }
}
