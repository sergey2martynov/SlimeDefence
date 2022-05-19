using CodeBase.Core.Character.Enemy;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private int _maxSize;
    [SerializeField] private Transform _parent;
    [SerializeField] private SpawnerEnemies _spawner;
    
    private IObjectPool<EnemyController> _pool;
    public IObjectPool<EnemyController> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<EnemyController>(CreateEnemy, ActionOnGet, ActionOnRelease, (obj
                ) => Destroy(obj), false, 15, _maxSize);
            }

            return _pool;
        }
    }
    private EnemyController CreateEnemy()
    {
        var enemy = Instantiate(_enemyController, _spawner.FindRandomPosition().position, Quaternion.identity, _parent); 
        return enemy;
    }

    private void ActionOnGet(EnemyController enemyController)
    {
        enemyController.transform.position = _spawner.FindRandomPosition().position;
        enemyController.gameObject.SetActive(true);
        enemyController.IsDie = false;
    }

    private void ActionOnRelease(EnemyController enemyController)
    {
        enemyController.gameObject.SetActive(false);
        enemyController.transform.position = new Vector3(0, -100, 0);
        _spawner.SpawnedEnemies.Remove(enemyController);
    }
}