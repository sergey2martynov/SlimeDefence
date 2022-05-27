using UnityEngine;

namespace CodeBase.Core.Character.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private SpawnerEnemies _spawnerEnemies;
        [SerializeField] private int _damage;
        [SerializeField] private SpawnObjectOfExperience _spawnObjectOfExpirience;

        public Health Health => _health;
        public bool IsDie { get; set; }
        public EnemyType EnemyType => _enemyType;
        public int Damage => _damage;
        
        public void Initialize(SpawnerEnemies spawnerEnemies, bool isDie, SpawnObjectOfExperience spawnObjectOfExpirience)
        {
            _spawnObjectOfExpirience = spawnObjectOfExpirience;
            _spawnerEnemies = spawnerEnemies;
            IsDie = isDie;
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
            _spawnObjectOfExpirience.SpawnObjOfExperienceForEnemy(transform, _enemyType);
            IsDie = true;
            _spawnerEnemies.EnemyPools[(int)_enemyType].Pool.Release(gameObject);
            _spawnerEnemies.SpawnedEnemies.Remove(this);
        }
    }
}