using System;
using Core.Character;
using StaticData;
using UnityEngine;
using UnityEngine.Serialization;
using Upgrade;

public class Health : Upgradable
{
    [SerializeField] private int _healthPoint;
    [SerializeField] private Defence _defence;
    [SerializeField] private HealthLevels _healthLevels;
    [SerializeField] private CharacterType _characterType;

    private int _enemyHealthPoint;

    public int HealthPoint => _healthPoint;

    public event Action HealthIsOver;
    public event Action<int> MaxHealthChanged;

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

        if (_characterType == CharacterType.Player)
        {
            SetHealthPoint();
            MaxLevel = _healthLevels.GetMaxNumberOfLevel();
        }
    }

    public override void Upgrade()
    {
        _currentLevel++;
        SetHealthPoint();
        MaxHealthChanged?.Invoke(_healthPoint);
    }

    public override UpgradeParametersBase GetUpgradeParameters()
    {
        return _healthLevels.GetHealthParameters(_currentLevel + 1);
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