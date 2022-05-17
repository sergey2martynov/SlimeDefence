using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Core.Character.Enemy;
using CodeBase.Core.Character.Player;

public class ProjectileWeapon : AbstractWeapon
{
    [SerializeField] private Transform _player;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private int _amount;
    [SerializeField] private float _spread;
    [SerializeField] private TargetType _isTarget;

    [SerializeField] private GameObject _char;


    private List<EnemyController> _enemies;
    private float _elapsedTime;
    private float _distance = 100;

    private void Start()
    {
        _capsuleCollider.radius = Range;
        _enemies = new List<EnemyController>();
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
        if (_elapsedTime > 0.5)
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
        if (_enemies.Count == 0 && _isTarget == TargetType.Nearest)
        {
            return;
        }

        Vector3 target = Vector3.zero;

        switch (_isTarget)
        {
            case TargetType.Nearest:
                target = FindNearbyEnemy().transform.position;
                break;
            case TargetType.Random:
                target = _char.transform.position - new Vector3( Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)).normalized * 50;
                break;
        }

        for (int i = 0; i < _amount; i++)
        {
            target = RotateDirection(target, _spread);
            
            var projectile = Instantiate(_projectile, _player.transform.position, Quaternion.identity);
            projectile.Damage = Damage;

            projectile.transform.DOMove(target, _distance / Rate);
        }
    }

    private Vector3 RotateDirection(Vector3 target, float angle)
    {
        Vector3 vector = Quaternion.AngleAxis(angle, Vector3.up) * target;
        return vector;
    }
}
