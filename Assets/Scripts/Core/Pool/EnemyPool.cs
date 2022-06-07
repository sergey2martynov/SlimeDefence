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
        poolObject.transform.position =
            FindSpawnRandomPosition() + _player.position;
        poolObject.gameObject.SetActive(true);
    }

    public override void ActionOnRelease(GameObject poolObject)
    {
        poolObject.GetComponent<Health>().ReturnHealthPoint();
        poolObject.SetActive(false);
        poolObject.transform.position = new Vector3(0, -100, 0);
        _spawnerEnemies.SpawnedEnemies.Remove(poolObject.GetComponent<Enemy>());
    }

    private Vector3 FindSpawnRandomPosition()
    {
        var vector = new Vector3(10, 0, 10);
        Vector3 turnedVector = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * vector;
        return turnedVector;
    }
}