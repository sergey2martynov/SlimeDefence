using System;
using System.Collections.Generic;
using CodeBase.Core.Character;
using UnityEngine;
using CodeBase.Core.Character.Enemy;
using UI.WeaponsPanel;
using UpgradeWeapon;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private WeaponsPanel _weaponsPanel;
    [SerializeField] private ProjectileWeaponLevels _weaponParameters;
    [SerializeField] private GunshotProjectilePool _gunshotProjectilePool;
    [SerializeField] private Movement _movement;
    [SerializeField] private Transform _player;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private int _amount;
    [SerializeField] private float _spread;
    [SerializeField] private TargetType _targetType;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _penetrationCounter;
     

    private ProjectileWeaponParameters _currentParameters;


    private List<Enemy> _enemies;
    private float _elapsedTime;
    private float _distance = 100;
    private Vector3 _direction;

    public List<Enemy> Enemies => _enemies;

    private void Awake()
    {
        if (IsActive)
        {
            _currentParameters = _weaponParameters.GetWeaponParameters(0);
            _upgradeParameters = _weaponParameters.GetWeaponParameters(_currentLevel + 1);
        }
        else
        {
            _currentParameters = _weaponParameters.GetWeaponParameters(0);
            _upgradeParameters = _weaponParameters.GetWeaponParameters(0);
        }
    }

    private void Start()
    {
        _enemies = new List<Enemy>();

        MaxLevel = _weaponParameters.GetMaxNumberOfLevel();

        SetState();
        _capsuleCollider.radius = Range;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        
        if (_enemies.Count == 0)
        {
            _movement.SetLookDirection(_movement.Direction, 1);
        }

        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i] == null || !_enemies[i].isActiveAndEnabled || _enemies[i].IsDie)
            {
                _enemies.Remove(_enemies[i]);
            }
        }
        
        if (_elapsedTime > Rate && IsActive && _enemies.Count != 0 && (_targetType == TargetType.Nearest || _targetType == TargetType.RandomEnemy))
        {
            UseWeapon();
            _elapsedTime = 0;
        }
    }

    private Enemy FindNearbyEnemy()
    {
        int indexOfEnemies = 0;
        
        float minDistance  = Vector3.Distance(_player.position, _enemies[0].transform.position);

        for (int i = 1; i < _enemies.Count; i++)
        {
                _distance = Vector3.Distance(_player.position, _enemies[i].transform.position);

                if (_distance < minDistance)
                {
                    minDistance = _distance;
                    indexOfEnemies = i;
                }
        }

        return _enemies[indexOfEnemies];
    }

    public override void UseWeapon()
    {
        if (_enemies.Count == 0 && (_targetType == TargetType.Nearest || _targetType == TargetType.RandomEnemy))
        {
            return;
        }

        var currentPosition = transform.position;

        _direction = _targetType switch
        {
            TargetType.Nearest => FindNearbyEnemy().transform.position - currentPosition + new Vector3(0, 1, 0),
            TargetType.RandomEnemy => _enemies[Random.Range(0, _enemies.Count)].transform.position - currentPosition +
                                      new Vector3(0, 1, 0).normalized,
            TargetType.Random => new Vector3
            (
                Random.Range(0, 2) == 0 ? Random.Range(3f, 5f) : Random.Range(-5f, -3f),
                0,
                Random.Range(0, 2) == 0 ? Random.Range(3f, 5f) : Random.Range(-5f, -3f)
            ).normalized,
            _ => _direction
        };

        _movement.SetLookDirection(_direction, 0.5f);

        for (int i = 1; i <= _amount; i++)
        {
            var rotatedDirection = RotateDirection(_direction, _spread * ((i - 1) / (float) _amount));
            var projectile = _gunshotProjectilePool.Pool.Get();

            projectile.GetComponent<Projectile>().Initialize(Damage, rotatedDirection, _gunshotProjectilePool, _player, 
                _projectileSpeed, _penetrationCounter);
        }
    }

    private Vector3 RotateDirection(Vector3 target, float angle)
    {
        Vector3 vector = Quaternion.AngleAxis(angle, Vector3.up) * target;
        return vector;
    }


    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            _enemies.Add(enemy);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     for (int i = 0; i < _enemies.Count; i++)
    //     {
    //         if (other.gameObject.GetComponent<Enemy>() == _enemies[i])
    //         {
    //             _enemies.Remove(_enemies[i]);
    //         }
    //     }
    // }

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
        
        SetState();
        _weaponsPanel.UpdatePanel(this, false);
    }

    private void SetState()
    {
        _amount = _currentParameters.Amount;
        _spread = _currentParameters.Spread;
        _damage = _currentParameters.Damage;
        _rate = _currentParameters.Rate;
        _range = _currentParameters.Range;
        _capsuleCollider.radius = _range;
        _projectileSpeed = _currentParameters.ProjectileSpeed;
        _penetrationCounter = _currentParameters.Penetration;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawRay(transform.position,transform.position + _direction * 50);
    //     for (int i = 1; i <= _amount; i++)
    //     {
    //         var rotatedDirection = RotateDirection(_direction, _spread * ((i-1)/ (float)_amount));
    //         Gizmos.DrawRay(transform.position,transform.position + rotatedDirection * 50);
    //     }
    // }
}