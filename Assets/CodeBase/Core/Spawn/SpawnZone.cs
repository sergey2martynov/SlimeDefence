using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    
    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < _spawnerEnemies.SpawnedEnemies.Count; i++)
        {
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            if (enemyController == _spawnerEnemies.SpawnedEnemies[i])
            {
                _spawnerEnemies.EnemyPools[(int)enemyController.EnemyType].Pool.Release(enemyController.gameObject);
            }
        }
    }
}
