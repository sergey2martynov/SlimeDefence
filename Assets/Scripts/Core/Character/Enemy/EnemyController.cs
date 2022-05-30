using UnityEngine;

namespace CodeBase.Core.Character.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _damage;
        private SpawnObjectOfExperience _spawnObjectOfExperience;
        private SpawnerEnemies _spawnerEnemies;

        public Health Health => _health;
        public bool IsDie { get;private set; }
        public EnemyType EnemyType => _enemyType;
        public int Damage => _damage;
        
        public void Initialize(SpawnObjectOfExperience spawnObjectOfExperience)
        {
            _spawnObjectOfExperience = spawnObjectOfExperience;
            IsDie = false;
        }
        
        private void Start()
        {
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
            
            if (_enemyType == EnemyType.MiniBoss || _enemyType == EnemyType.Boss)
            {
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
    }
}