using System;
using DG.Tweening;
using UI.WeaponsPanel;
using Unity.Mathematics;
using UnityEngine;
using UpgradeWeapon;
using Random = UnityEngine.Random;

public class RailGun : Weapon
{
    [SerializeField] private EnemiesCounter _enemiesCounter;
    [SerializeField] private RailGunLevels _railGunLevels;
    [SerializeField] private ParticleSystem _target;
    [SerializeField] private ParticleSystem _projectile;
    [SerializeField] private WeaponsPanel _weaponsPanel;
    private float _projectileSpeed;
    private float _elapsedTime;
    private ParticleSystem _particleTarget;
    private RailGunTarget _railGunTarget;
    private RailGunProjectile _railGunProjectile;
    private RailGunParameters _currentParameters;

    private void Start()
    {
        _particleTarget = Instantiate(_target, new Vector3(0,0, 200), Quaternion.identity);
        _particleTarget.gameObject.SetActive(false);
        _upgradeParameters = _railGunLevels.GetWeaponParameters(_currentLevel);
        _currentParameters = _railGunLevels.GetWeaponParameters(_currentLevel);
        _railGunTarget = _particleTarget.GetComponent<RailGunTarget>();
        _projectileSpeed = _currentParameters.ProjectileSpeed;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
            
        for (int i = 0; i < _enemiesCounter.EnemiesOnScreen.Count; i++)
        {
            if (_enemiesCounter.EnemiesOnScreen[i] == null || !_enemiesCounter.EnemiesOnScreen[i].isActiveAndEnabled || _enemiesCounter.EnemiesOnScreen[i].IsDie)
            {
                _enemiesCounter.EnemiesOnScreen.Remove(_enemiesCounter.EnemiesOnScreen[i]);
            }
        }

        if (_elapsedTime > _rate && _enemiesCounter.EnemiesOnScreen.Count != 0 && IsActive)
        {
            UseWeapon();
            _elapsedTime = 0;
        }
    }
    
    public override void UseWeapon()
    {
        ChooseTarget();

        DOTween.Sequence().AppendInterval(2f).OnComplete(() =>
        {
            var projectile = Instantiate(_projectile, transform.position, quaternion.identity);
            projectile.GetComponent<RailGunProjectile>().Initialize(_damage,_railGunTarget.transform.position, transform, _projectileSpeed );
            _railGunTarget.gameObject.SetActive(false);
        });
    }

    private void ChooseTarget()
    {
        var enemy = _enemiesCounter.EnemiesOnScreen[Random.Range(0, _enemiesCounter.EnemiesOnScreen.Count)];
        _railGunTarget.SetTarget(enemy.transform);
        enemy.ISTargetRailGun = true;
        _railGunTarget.gameObject.SetActive(true);
        _particleTarget.Play();
    }
    
    public override void Upgrade()
    {
        if (!IsActive)
        {
            IsActive = true;
            _elapsedTime = 0;
            _upgradeParameters = _railGunLevels.GetWeaponParameters(_currentLevel + 1);
            _weaponsPanel.UpdatePanel(this, true);
            return;
        }

        base.Upgrade();
        _currentParameters = _railGunLevels.GetWeaponParameters(_currentLevel);
        if (_currentLevel < MaxLevel)
            _upgradeParameters = _railGunLevels.GetWeaponParameters(_currentLevel + 1);

        _rate = _currentParameters.Rate;
        _weaponsPanel.UpdatePanel(this, false);
    }
}
