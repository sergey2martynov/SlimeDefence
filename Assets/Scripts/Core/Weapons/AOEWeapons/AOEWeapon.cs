using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using DG.Tweening;
using UI.WeaponsPanel;
using UnityEngine;
using UpgradeWeapon;
using Random = UnityEngine.Random;

namespace Core.Weapons
{
    public class AOEWeapon : Weapon
    {
        [SerializeField] private ParticleSystem _projectile;
        [SerializeField] private SunStrikeLevels _weaponParameters;
        [SerializeField] private WeaponsPanel _weaponsPanel;
        [SerializeField] private EnemiesCounter _enemiesCounter;
        
        private SphereCollider _sphereCollider;
        private ParticleSystem _spawnedProjectile;
        private WeaponParameters _currentParameters;
        private float _elapsedTime;
        
        

        private void Start()
        {
            _spawnedProjectile = Instantiate(_projectile, new Vector3(0,0, 200), Quaternion.identity);
            _spawnedProjectile.GetComponent<SunStrikeProjectile>().Initialize(_damage);
            _sphereCollider = _spawnedProjectile.GetComponent<SphereCollider>();
            _spawnedProjectile.gameObject.SetActive(false);
            _sphereCollider.enabled = false;
            _upgradeParameters = _weaponParameters.GetWeaponParameters(_currentLevel);
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
        
        public override void Upgrade()
        {
            if (!IsActive)
            {
                IsActive = true;
                _elapsedTime = 0;
                _upgradeParameters = _weaponParameters.GetWeaponParameters(_currentLevel + 1);
                _weaponsPanel.UpdatePanel(this, true);
                return;
            }

            base.Upgrade();
            _currentParameters = _weaponParameters.GetWeaponParameters(_currentLevel);
            if (_currentLevel < MaxLevel)
                _upgradeParameters = _weaponParameters.GetWeaponParameters(_currentLevel + 1);

            _rate = _currentParameters.Rate;
            _weaponsPanel.UpdatePanel(this, false);
        }

        public override void UseWeapon()
        {
            _spawnedProjectile.gameObject.transform.position =
                _enemiesCounter.EnemiesOnScreen[Random.Range(0, _enemiesCounter.EnemiesOnScreen.Count)].transform.position;
            _spawnedProjectile.gameObject.SetActive(true);
            _spawnedProjectile.Stop();
            _spawnedProjectile.Play();
            DOTween.Sequence().AppendInterval(0.5f).OnComplete(() =>
            {
                _sphereCollider.enabled = true;
                DOTween.Sequence().AppendInterval(1f).OnComplete(() =>
                {
                    _spawnedProjectile.gameObject.SetActive(false);
                    _sphereCollider.enabled = false;
                });
            }); 
        }
    }
}