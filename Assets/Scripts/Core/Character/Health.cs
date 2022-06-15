using System;
using Core.Character;
using DG.Tweening;
using StaticData;
using Unity.Mathematics;
using UnityEngine;
using Upgrade;

public class Health : Upgradable
{
    [SerializeField] private int _healthPoint;
    [SerializeField] private Defence _defence;
    [SerializeField] private HealthLevels _healthLevels;
    [SerializeField] private CharacterType _characterType;
    [SerializeField] private BloodSplat _bloodSplat;

    private int _enemyHealthPoint;
    public int HealthPoint => _healthPoint;
    public event Action HealthIsOver;
    public event Action<int> MaxHealthChanged;
    public event Action<float> HealthChanged;
    public BloodSplatPool BloodSplatPool { get; set; }

    public void GetDamage(int damageTaken)
    {
        if (_characterType == CharacterType.Enemy)
        {
            var particle = BloodSplatPool.Pool.Get();
            particle.GetComponent<BloodSplat>().Initialize(transform);
        }
        
        _healthPoint = _healthPoint - damageTaken + _defence.DefencePlayer;
        HealthChanged?.Invoke(damageTaken);

        if (_healthPoint <= 0)
        {
            HealthIsOver?.Invoke();
        }
    }
    
    private void Start()
    {
        _enemyHealthPoint = _healthPoint;

        if (_characterType == CharacterType.Player)
        {
            MaxLevel = _healthLevels.GetMaxNumberOfLevel();
            SetHealthPoint();
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