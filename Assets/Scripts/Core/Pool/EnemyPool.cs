using CodeBase.Core;
using CodeBase.Core.Character.Enemy;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : AbstractPool
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private Transform _player;

    public override GameObject CreateObject()
    {
        var poolObject = Instantiate(_poolObject,
            RandomPositionFinder.FindRandomPosition(transform, _player, -41, 15, -21, 21).position, Quaternion.identity,
            _parent);
        return poolObject;
    }

    public override void ActionOnGet(GameObject poolObject)
    {
        poolObject.transform.position =
            RandomPositionFinder.FindRandomPosition(transform, _player, -41, 15, -21, 21).position;
        poolObject.gameObject.SetActive(true);
        poolObject.GetComponent<EnemyController>().IsDie = false;
    }

    public override void ActionOnRelease(GameObject poolObject)
    {
        poolObject.GetComponent<Health>().ReturnHealthPoint();
        poolObject.SetActive(false);
        poolObject.transform.position = new Vector3(0, -100, 0);
        _spawnerEnemies.SpawnedEnemies.Remove(poolObject.GetComponent<EnemyController>());
    }
}