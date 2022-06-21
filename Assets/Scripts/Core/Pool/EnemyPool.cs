using CodeBase.Core;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class EnemyPool : AbstractPool
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private Transform _player;

    public override void Release(GameObject poolObject)
    {
        
        for (int i = 0; i < _pool.Count; i++)
        {
            if (poolObject == _pool[i])
            {
                poolObject.GetComponent<Health>().ReturnHealthPoint();
                _spawnerEnemies.SpawnedEnemies.Remove(poolObject.GetComponent<Enemy>());
                poolObject.SetActive(false);
                return;
            }
        }
        
        
    }
}