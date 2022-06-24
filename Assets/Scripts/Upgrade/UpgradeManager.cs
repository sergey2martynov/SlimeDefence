using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrade;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Upgradable> _upgradables;
    [SerializeField] private List<Upgradable> _newWeapons;

    private int _currentWave;
    public List<Upgradable>NewWeapons => _newWeapons;

    public Upgradable GetNewWeapon()
    {
        var weapon = _newWeapons[0];
        _newWeapons.Remove(_newWeapons[0]);
        _upgradables.Add(weapon);

        return weapon;
    }
    
    public List<Upgradable> GetNewLevelUpgrades()
    {
        
        for (int i = 0; i < _upgradables.Count; i++)
        {
            if (_upgradables[i].MaxLevel == _upgradables[i].CurrentLevel)
            {
                _upgradables.Remove(_upgradables[i]);
            }
        }
        
        List<Upgradable> returnedUpgradables = new List<Upgradable>();
        List<Upgradable> upgradables = new List<Upgradable>(_upgradables);

        for (int i  = 0; i  < 3; i ++)
        {
            int index = Random.Range(0, upgradables.Count);

            returnedUpgradables.Add(upgradables[index]);
            upgradables.Remove(upgradables[index]);
        }

        return returnedUpgradables;
    }
}
