using Core.Environment;
using Core.Expirience;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Core.Character.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _damage;
        [SerializeField] private int _dropeChance;
        [SerializeField] private Experience _experience;
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;
        
        private SpawnerEnemies _spawnerEnemies;
        private KillCounter _killCounter;
        private WinScreen _winScreen;
        private ExperiencePool _experiencePool;
        private HealthBox _healthBox;
        private Transform _healthBoxParent;
        private Camera _camera;
        private Color _color;

        public Health Health => _health;
        public bool IsDie { get;private set; }
        public EnemyType EnemyType => _enemyType;
        public int Damage => _damage;

        public Camera Camera => _camera;
        public SkinnedMeshRenderer MeshRenderer => _meshRenderer;

        public void Initialize(KillCounter killCounter, WinScreen winScreen, ExperiencePool pool, HealthBox healthBox, Transform healthBoxParent, Camera camera)
        {
            _killCounter = killCounter;
            IsDie = false;
            _winScreen = winScreen;
            _experiencePool = pool;
            _healthBox = healthBox;
            _healthBoxParent = healthBoxParent;
            _camera = camera;
        }
        
        private void Start()
        {
            _color = _meshRenderer.material.color;
            _health.HealthIsOver += Die;
        }

        private void OnDestroy()
        {
            _health.HealthIsOver -= Die;
        }

        private void Die()
        {
            IsDie = true;
            _killCounter.IncreaseCounter();

            if (_enemyType == EnemyType.MiniBoss)
            {
                var experience = Instantiate(_experience, transform.position, Quaternion.identity);
                
                experience.Initialize(_experiencePool, gameObject.GetComponent<EnemyMovementInput>().Target);
                
                Destroy(gameObject);
            }
            else if (_enemyType == EnemyType.Boss)
            {
                Destroy(gameObject);
            }
            else
            {
                var calculatedChance = Random.Range(0, 100);
                
                if (calculatedChance < _dropeChance)
                {
                    if (calculatedChance == 49)
                        Instantiate(_healthBox, transform.position, Quaternion.identity, _healthBoxParent);
                    else
                        SpawnObjOfExperience();
                }
                
                _spawnerEnemies.EnemyPools[(int)_enemyType].Pool.Release(gameObject);
                _spawnerEnemies.SpawnedEnemies.Remove(this);
            }
        }

        public void SetSpawnerEnemiesRef(SpawnerEnemies spawnerEnemies)
        {
            _spawnerEnemies = spawnerEnemies;
        }
        
        public void SpawnObjOfExperience()
        {
            GameObject objectOfExperience;
        
            objectOfExperience = _experiencePool.Pool.Get();
            objectOfExperience.transform.position = transform.position;
        }

        public void ReturnColor()
        {
            _meshRenderer.material.color = _color;
        }
    }
}