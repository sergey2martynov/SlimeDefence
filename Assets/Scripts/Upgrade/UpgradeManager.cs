using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrade;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Upgradable> _upgradables;
    [SerializeField] private List<Upgradable> _newWeapons;

    private int _currentWave;

    public Upgradable GetNewWeapon()
    {
        var weapon = _newWeapons[0];
        _newWeapons.Remove(_newWeapons[0]);
        _upgradables.Add(weapon);

        return weapon;
    }
    
    public List<Upgradable> GetNewLevelUpgrades()
    {
        List<Upgradable> returnedUpgradables = new List<Upgradable>();
        List<Upgradable> upgradables = new List<Upgradable>(_upgradables);
        
        IEnumerable<Upgradable> upgradablesWithMaxLevel =  _upgradables.Where(upgradable => upgradable.MaxLevel == upgradable.CurrentLevel);

        foreach (var upgradable in upgradablesWithMaxLevel)
        {
            _upgradables.Remove(upgradable);
            break;
        }

        for (int i  = 0; i  < 3; i ++)
        {
            int index = Random.Range(0, upgradables.Count);

            returnedUpgradables.Add(upgradables[index]);
            upgradables.Remove(upgradables[index]);
        }

        return returnedUpgradables;
    }
}
