using System.Collections.Generic;
using UnityEngine;
using Upgrade;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Upgradable> _activeUpgradableObjects;
    [SerializeField] private List<Upgradable> _inactiveUpgradableObjects;
    [SerializeField] private int _maxUpgradableObjects = 9;

    public List<Upgradable> GetNewLevelUpgrades()
    {
        CheckOfActiveObjects();
        
        List<Upgradable> combainedUpgradables = new List<Upgradable>();
        
        List<Upgradable> returnedUpgradables = new List<Upgradable>();
        
        combainedUpgradables.AddRange(_activeUpgradableObjects);

        if (_activeUpgradableObjects.Count < _maxUpgradableObjects)
        {
            combainedUpgradables.AddRange(_inactiveUpgradableObjects);
        }

        for (int i  = 0; i  < 3; i ++)
        {
            int index = Random.Range(0, combainedUpgradables.Count);
            
            returnedUpgradables.Add(combainedUpgradables[index]);
            combainedUpgradables.Remove(combainedUpgradables[index]);
        }

        return returnedUpgradables;
    }

    private void CheckOfActiveObjects()
    {
        for (int i = 0; i < _inactiveUpgradableObjects.Count; i++)
        {
            if (_inactiveUpgradableObjects[i].CurrentLevel == 1)
            {
                _activeUpgradableObjects.Add(_inactiveUpgradableObjects[i]);
                _inactiveUpgradableObjects.Remove(_inactiveUpgradableObjects[i]);
            }
        }
    }
}
