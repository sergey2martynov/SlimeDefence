using CodeBase.Core;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class EnemyPool : AbstractPool
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private Transform _player;

    public override GameObject CreateObject()
    {
        var poolObject = Instantiate(_poolObject,
            FindSpawnRandomPosition() + _player.position, Quaternion.identity,
            _parent);
        return poolObject;
    }

    public override void ActionOnGet(GameObject poolObject)
    {
        if (poolObject.activeSelf)
            return;
        poolObject.transform.position =
            FindSpawnRandomPosition() + _player.position;
        poolObject.gameObject.SetActive(true);

        poolObject.GetComponent<EnemyMovementInput>().MoveEnemy();
    }

    public override void ActionOnRelease(GameObject poolObject)
    {
        poolObject.GetComponent<Health>().ReturnHealthPoint();
        poolObject.SetActive(false);
        _spawnerEnemies.SpawnedEnemies.Remove(poolObject.GetComponent<Enemy>());
    }

    private Vector3 FindSpawnRandomPosition()
    {
        Vector3 vector = new Vector3(
            Random.Range(-15, 15),
            0,
            Random.Range(0, 2) == 0 ? Random.Range(-30, -20) : Random.Range(30, 20));

        Vector3 turnedVector = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * vector;
        return vector;
    }
}