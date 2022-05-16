using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Core.Character.Enemy;
using CodeBase.Core.Character.Player;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private PlayerControler _player;

    private List<EnemyController> _enemies;

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        
        if (enemy != null)
        {
            _enemies.Add(enemy);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        foreach (var enemy in _enemies)
        {
            if (other.gameObject.GetComponent<EnemyController>() == enemy)
            {
                _enemies.Remove(enemy);
            }
        }
    }

    private Vector3 FindNearbyEnemy()
    {
        float minDistance = Vector2.Distance(_player.transform.position, _enemies[0].transform.position);
        float distance;
        int indexOfEnemies = 0;

        for (int i = 1; i < _enemies.Count; i++)
        {
            distance = Vector2.Distance(_player.transform.position, _enemies[i].transform.position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                indexOfEnemies = i;
            }
        }

        return _enemies[indexOfEnemies].transform.position;
    }
    
    private void Fire()
    {
        
    }
}
