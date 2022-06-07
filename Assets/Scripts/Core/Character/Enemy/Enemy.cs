using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Core.Character.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Movement _movement;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _damage;
        private SpawnObjectOfExperience _spawnObjectOfExperience;
        private SpawnerEnemies _spawnerEnemies;
        private KillCounter _killCounter;
        private List<Enemy> _enemiesAround;
        private WinScreen _winScreen;

        public Health Health => _health;
        public Movement Movement => _movement;
        public bool IsDie { get;private set; }
        public EnemyType EnemyType => _enemyType;
        public int Damage => _damage;

        public List<Enemy> EnemiesAround => _enemiesAround;

        public void Initialize(SpawnObjectOfExperience spawnObjectOfExperience, KillCounter killCounter, WinScreen winScreen)
        {
            _spawnObjectOfExperience = spawnObjectOfExperience;
            _killCounter = killCounter;
            IsDie = false;
            _winScreen = winScreen;
        }
        
        private void Start()
        {
            _enemiesAround = new List<Enemy>();
            _health.HealthIsOver += Die;
        }

        private void OnDestroy()
        {
            _health.HealthIsOver -= Die;
        }

        private void Die()
        {
            _spawnObjectOfExperience.SpawnObjOfExperienceForEnemy(transform, _enemyType);
            IsDie = true;
            _killCounter.IncreaseCounter();
            
            if (_enemyType == EnemyType.MiniBoss)
            {
                Destroy(gameObject);
            }
            else if (_enemyType == EnemyType.Boss)
            {
                _winScreen.ShowWinScreen(true);
                Destroy(gameObject);
            }
            else
            {
                _spawnerEnemies.EnemyPools[(int)_enemyType].Pool.Release(gameObject);
                _spawnerEnemies.SpawnedEnemies.Remove(this);
            }
        }

        public void SetSpawnerEnemiesRef(SpawnerEnemies spawnerEnemies)
        {
            _spawnerEnemies = spawnerEnemies;
        }

        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.gameObject.TryGetComponent(out Enemy enemy))
        //         _enemiesAround.Add(enemy);
        // }
        //
        // private void OnCollisionExit(Collision other)
        // {
        //     if (other.gameObject.TryGetComponent(out Enemy enemy))
        //         _enemiesAround.Remove(enemy);
        // }
    }
}