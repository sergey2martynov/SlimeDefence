using System.Collections.Generic;
using UnityEngine;
using CodeBase.Core.Character.Enemy;
using UpgradeWeapon;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ProjectileWeapon : AbstractWeapon
{
    [SerializeField] private ProjectileWeaponLevels _weaponParameters;
    [SerializeField] private GunshotProjectilePool _gunshotProjectilePool;
    [SerializeField] private Transform _player;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private int _amount;
    [SerializeField] private float _spread;
    [SerializeField] private TargetType _targetType;
    [SerializeField] private float _projectileSpeed;

    private ProjectileWeaponParameters _currentParameters;


    private List<EnemyController> _enemies;
    private float _elapsedTime;
    private float _distance = 100;
    private Vector3 _direction;

    private void Start()
    {
        _currentParameters = _weaponParameters.GetWeaponParameters(_currentLevel);
        SetState();
        _capsuleCollider.radius = Range;
        _enemies = new List<EnemyController>();
    }

    private void Update()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].IsDie)
            {
                _enemies.Remove(_enemies[i]);
            }
        }

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > Rate)
        {
            UseWeapon();
            _elapsedTime = 0;
        }
    }

    private EnemyController FindNearbyEnemy()
    {
        float minDistance = Vector2.Distance(_player.position, _enemies[0].transform.position);
        int indexOfEnemies = 0;

        for (int i = 1; i < _enemies.Count; i++)
        {
            _distance = Vector2.Distance(_player.position, _enemies[i].transform.position);

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
        if (_enemies.Count == 0 && _targetType == TargetType.Nearest)
        {
            return;
        }

        _direction = _targetType switch
        {
            TargetType.Nearest => FindNearbyEnemy().transform.position - transform.position + new Vector3(0, 2, 0),
            TargetType.Random => new Vector3
            (
                Random.Range(0, 2) == 0 ? Random.Range(3f, 5f) : Random.Range(-5f, -3f),
                0,
                Random.Range(0, 2) == 0 ? Random.Range(3f, 5f) : Random.Range(-5f, -3f)
            ).normalized,
            _ => _direction
        };

        for (int i = 1; i <= _amount; i++)
        {
            var rotatedDirection = RotateDirection(_direction, _spread * ((i - 1) / (float) _amount));
            var projectile = _gunshotProjectilePool.Pool.Get();

            projectile.GetComponent<Projectile>().Initialize(Damage, rotatedDirection, _gunshotProjectilePool, _player,
                _projectileSpeed);
        }
    }

    private Vector3 RotateDirection(Vector3 target, float angle)
    {
        Vector3 vector = Quaternion.AngleAxis(angle, Vector3.up) * target;
        return vector;
    }


    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.gameObject.GetComponent<EnemyController>();

        if (enemy != null)
        {
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (other.gameObject.GetComponent<EnemyController>() == _enemies[i])
            {
                _enemies.Remove(_enemies[i]);
            }
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        _currentParameters = _weaponParameters.GetWeaponParameters(_currentLevel);
        SetState();
    }

    private void SetState()
    {
        _amount = _currentParameters.Amount;
        _spread = _currentParameters.Spread;
        _damage = _currentParameters.Damage;
        _rate = _currentParameters.Rate;
        _range = _currentParameters.Range;
        _capsuleCollider.radius = _range;
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