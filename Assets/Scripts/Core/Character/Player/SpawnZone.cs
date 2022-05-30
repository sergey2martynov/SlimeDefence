using CodeBase.Core.Character.Enemy;
using Core.Environment;
using Core.Environment.Chest;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private SpawnerObstacles _spawnObstacles;
    [SerializeField] private SpawnerСhest _spawnerСhest;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            if (enemyController.EnemyType != EnemyType.MiniBoss && enemyController.EnemyType != EnemyType.Boss)
            {
                _spawnerEnemies.EnemyPools[(int) enemyController.EnemyType].Pool.Release(enemyController.gameObject);
            }
        }
        
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _spawnObstacles.ChangePositionObstacle(obstacle);
        }
        
        if (other.gameObject.TryGetComponent(out Chest chest))
        {
            _spawnerСhest.ChangePositionChest(chest);
        }
    }
}