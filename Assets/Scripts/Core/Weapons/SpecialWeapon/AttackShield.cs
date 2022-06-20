using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using Core.Character.Player;
using UI.WeaponsPanel;
using UnityEngine;
using UpgradeWeapon;

public class AttackShield : Weapon
{
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private AttackShieldLevels _levels;
    [SerializeField] private WeaponsPanel _weaponsPanel;
    [SerializeField] private Transform _player;
    [SerializeField] private ParticleSystem _circle;
    [SerializeField] private AttackSpeed _attackSpeed;

    private float _fixedTime;
    private WeaponParameters _currentParameters;
    private List<Enemy> _enemiesAround;
    private ParticleSystem.MainModule _circleMain;

    private void Start()
    {
        _enemiesAround = new List<Enemy>();
        _currentParameters = _levels.GetWeaponParameters(_currentLevel);
        _collider.radius = _currentParameters.Range;
        _damage = _currentParameters.Damage;
        _upgradeParameters = _levels.GetWeaponParameters(_currentLevel + 1);
        _circleMain = _circle.main;
        _circleMain.startSize = _currentParameters.Range * 2.7f;
        MaxLevel = _levels.GetMaxNumberOfLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            _enemiesAround.Add(enemy);
    }

    private void Update()
    {
        if (Time.time - _fixedTime > _rate * 1 / _attackSpeed.AttackSpeedValue)
        {
            for (int i = 0; i < _enemiesAround.Count; i++)
            {
                if (_enemiesAround[i] == null || _enemiesAround[i].IsDie ||
                    (_player.position - _enemiesAround[i].transform.position).magnitude > _currentParameters.Range + 1)
                    _enemiesAround.Remove(_enemiesAround[i]);
                else
                {
                    _enemiesAround[i].Health.GetDamage(_damage);
                    _fixedTime = Time.time;
                }
            }
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        _currentParameters = _levels.GetWeaponParameters(_currentLevel);
        if (_currentLevel < MaxLevel)
            _upgradeParameters = _levels.GetWeaponParameters(_currentLevel + 1);
        _damage = _currentParameters.Damage;
        _collider.radius = _currentParameters.Range;
        _weaponsPanel.UpdatePanel(this, false);
        _circleMain.startSize = _currentParameters.Range * 2.7f;
    }
}