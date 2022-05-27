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
    
    private int _enemyHealthPoint;
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
        _enemyHealthPoint = _healthPoint;
        
        if (_movementType == MovementType.PlayerMovement)
        {
            SetHealthPoint();
        }
    }

    public void Upgrade()
    {
        _currentLevel++;
        SetHealthPoint();
    }

    public UpgradeParametersBase GetUpgradeParameters()
    {
        return _healthLevels.GetHealthParameters(_currentLevel+1);
    }

    public void ReturnHealthPoint()
    {
        _healthPoint = _enemyHealthPoint;
    }

    public void SetHealthPoint()
    {
        _healthPoint = _healthLevels.GetHealthParameters(_currentLevel).Amount;
    }
}
