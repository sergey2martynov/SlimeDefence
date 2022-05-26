using System;
using Core.Character;
using StaticData;
using UnityEngine;
using Upgrade;

public class Health : MonoBehaviour, IUpgradable
{
    [SerializeField] private int _healthPoint;
    [SerializeField] private Defence _defence;
    [SerializeField] private HealthLevels _healthLevels;
    [SerializeField] private MovementType _movementType;
    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public int HealthPoint => _healthPoint;

    public event Action HealthIsOver;

    public void GetDamage(int damageTaken)
    {
        _healthPoint = _healthPoint - damageTaken + _defence.DefencePlayer;
        
        if (_healthPoint < 0)
        {
            HealthIsOver?.Invoke();
        }
    }

    private void Start()
    {
        if (_movementType == MovementType.PlayerMovement)
        {
            _healthPoint = _healthLevels.GetHealthParameters(_currentLevel).Amount;
        }
    }

    public void Upgrade()
    {
        _currentLevel++;
        _healthPoint = _healthLevels.GetHealthParameters(_currentLevel).Amount;
    }

    public UpgradeParametersBase GetUpgradeParameters()
    {
        return _healthLevels.GetHealthParameters(_currentLevel+1);
    }
}
