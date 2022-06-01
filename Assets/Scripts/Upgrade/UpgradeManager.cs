using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrade;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Upgradable> _upgradableObjects;
    [SerializeField] private int _maxUpgradableObjects = 9;

    public List<Upgradable> GetNewLevelUpgrades()
    {

        List<Upgradable> upgradables = new List<Upgradable>();
        List<Upgradable> returnedUpgradables = new List<Upgradable>();
        
        IEnumerable<Upgradable> upgradablesWithMaxLevel =  _upgradableObjects.Where(upgradable => upgradable.MaxLevel == upgradable.CurrentLevel);

        foreach (var upgradable in upgradablesWithMaxLevel)
        {
            _upgradableObjects.Remove(upgradable);
            break;
        }

        if (_upgradableObjects.Count(upgradable => upgradable.IsActive) < _maxUpgradableObjects)
        {
            upgradables = new List<Upgradable>(_upgradableObjects);
        }
        else
        {
            IEnumerable<Upgradable> activeUpgradables =  _upgradableObjects.Where(upgradable => upgradable.IsActive);

            foreach (var upgradable in activeUpgradables)
            {
                upgradables.Add(upgradable);
            }
            
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
