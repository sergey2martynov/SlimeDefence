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
        
        private SphereCollider _sphereCollider;
        private ParticleSystem _spawnedProjectile;
        private WeaponParameters _currentParameters;
        private float _elapsedTime;
        
        public List<Enemy> EnemiesOnScreen { get; set; }

        private void Start()
        {
            EnemiesOnScreen = new List<Enemy>();
            _spawnedProjectile = Instantiate(_projectile, new Vector3(0,0, 200), Quaternion.identity);
            _spawnedProjectile.GetComponent<SunStrikeProjectile>().Initialize(_damage);
            _sphereCollider = _spawnedProjectile.GetComponent<SphereCollider>();
            _spawnedProjectile.gameObject.SetActive(false);
            _upgradeParameters = _weaponParameters.GetWeaponParameters(_currentLevel);
        }
        
        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            
            for (int i = 0; i < EnemiesOnScreen.Count; i++)
            {
                if (EnemiesOnScreen[i] == null || !EnemiesOnScreen[i].isActiveAndEnabled || EnemiesOnScreen[i].IsDie)
                {
                    EnemiesOnScreen.Remove(EnemiesOnScreen[i]);
                }
            }

            if (_elapsedTime > _rate && EnemiesOnScreen.Count != 0 && IsActive)
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
                EnemiesOnScreen[Random.Range(0, EnemiesOnScreen.Count)].transform.position;
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