using CodeBase.Core.Character.Enemy;
using Core.Environment;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private SpawnObstacles _spawnObstacles;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            _spawnerEnemies.EnemyPools[(int) enemyController.EnemyType].Pool.Release(enemyController.gameObject);
        }
        
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _spawnObstacles.ChangePositionObstacle(obstacle);
        }
    }
}