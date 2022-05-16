using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Core.Character.Enemy;
using CodeBase.Core.Character.Player;

public class ProjectileWeapon : AbstractWeapon
{
    [SerializeField] private PlayerControler _player;
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private float _speed;

    private List<EnemyController> _enemies;
    private float _elapsedTime;
    private float _distance;

    private void Start()
    {
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
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.5)
        {
            Fire();
            _elapsedTime = 0; 
        }
    }

    private EnemyController FindNearbyEnemy()
    {
        float minDistance = Vector2.Distance(_player.transform.position, _enemies[0].transform.position);
        float distance;
        int indexOfEnemies = 0;

        for (int i = 1; i < _enemies.Count; i++)
        {
            _distance = Vector2.Distance(_player.transform.position, _enemies[i].transform.position);
            
            if (_distance < minDistance)
            {
                minDistance = _distance;
                indexOfEnemies = i;
            }
        }

        return _enemies[indexOfEnemies];
    }
    
    private void Fire()
    {
        if (_enemies.Count == 0)
        {
            return;
        }
        var projectile = Instantiate(_projectilePrefab, _player.transform.position, Quaternion.identity);

        projectile.transform.DOMove(FindNearbyEnemy().transform.position, _distance / _speed);

    }
}
