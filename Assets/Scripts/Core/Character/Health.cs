using System;
using CodeBase.Core.Character.Enemy;
using Core.Character;
using DG.Tweening;
using StaticData;
using UnityEngine;
using Upgrade;

public class Health : Upgradable
{
    [SerializeField] private int _healthPoint;
    [SerializeField] private Defence _defence;
    [SerializeField] private HealthLevels _healthLevels;
    [SerializeField] private CharacterType _characterType;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private HealthBarFiller _healthBarFiller;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _bonusHealthMultiplier = 0.4f;

    private int _enemyHealthPoint;
    private int _currentMaxHealth;
    public int HealthPoint => _healthPoint;
    public int EnemyHealthPoint => _enemyHealthPoint;

    public event Action HealthIsOver;
    public event Action<int> MaxHealthChanged;
    public event Action<float> HealthChanged;
    public BloodSplatPool BloodSplatPool { get; set; }

    public void GetDamage(int damageTaken)
    {
        if (_characterType == CharacterType.Enemy)
        {
            _healthBarFiller.gameObject.SetActive(true);

            var particle = BloodSplatPool.Get();
            particle.GetComponent<BloodSplat>().Initialize(transform);

            _enemy.MeshRenderer.material.color = Color.white;

            DOTween.Sequence().AppendInterval(0.07f).OnComplete(() => { _enemy.ReturnColor(); });
        }

        _healthPoint = _healthPoint - damageTaken + _defence.DefencePlayer;
        HealthChanged?.Invoke(damageTaken);

        if (_healthPoint <= 0 && !_enemy.IsDie)
        {
            if (_healthBarFiller != null)
            {
                _healthBarFiller.gameObject.SetActive(false);
            }

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
        _currentMaxHealth = _healthPoint;
    }

    public void SetNewHealthPoint(int health)
    {
        _healthPoint = health;
    }

    public void AddBonusHealth()
    {
        _healthPoint += Convert.ToInt32(_currentMaxHealth * _bonusHealthMultiplier);

        if (_healthPoint > _currentMaxHealth)
            _healthPoint = _currentMaxHealth;
    }
}