using System.Collections.Generic;
using CodeBase.Core.Character;
using UnityEngine;
using Upgrade;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Defence _defence;
    [SerializeField] private Health _health;
    [SerializeField] private Movement _movement;
    [SerializeField] private AbstractWeapon _gun;
    [SerializeField] private AbstractWeapon _shotGun;
    [SerializeField] private int _maxUpgradableObjects = 9;

    private List<IUpgradable> _activeUpgradableObjects;
    private List<IUpgradable> _inactiveUpgradableObjects;
    private void Start()
    {
        _activeUpgradableObjects = new List<IUpgradable>() {_health, _defence, _movement, _gun};
        _inactiveUpgradableObjects = new List<IUpgradable>() {_shotGun};
    }

    public List<IUpgradable> GetNewLevelUpgrades()
    {
        CheckOfActiveObjects();
        
        List<IUpgradable> combainedUpgradables = new List<IUpgradable>();
        
        List<IUpgradable> returnedUpgradables = new List<IUpgradable>();
        
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
