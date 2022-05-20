using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private SpawnObjectOfExpirience _spawnObjectOfExpirience;
    [SerializeField] private List<EnemyPool> _enemyPools;
    [SerializeField] private int _maxNumberOfEnemies;
    [SerializeField] private Transform _player;
    [SerializeField] private float _spawnTimeForWeakEnemy;
    [SerializeField] private float _spawnTimeForAverageEnemy;
    [SerializeField] private float _spawnTimeForStrongEnemy;

    private float _elapsedTimeForWeak;
    private float _elapsedTimeForAverage;
    private float _elapsedTimeForStrong;

    private List<EnemyController> _spawnedEnemies;
    public List<EnemyController> SpawnedEnemies => _spawnedEnemies;
    public List<EnemyPool> EnemyPools => _enemyPools;

    private void Start()
    {
        _spawnedEnemies = new List<EnemyController>();
    }

    private void Update()
    {
        _elapsedTimeForWeak += Time.deltaTime;
        _elapsedTimeForAverage += Time.deltaTime;
        _elapsedTimeForStrong += Time.deltaTime;

        if (_spawnedEnemies.Count < _maxNumberOfEnemies)
        {
            if (_elapsedTimeForWeak > _spawnTimeForWeakEnemy)
            {
                SpawnEnemy(EnemyType.Weak);
                _elapsedTimeForWeak = 0;
            }

            if (_elapsedTimeForAverage > _spawnTimeForAverageEnemy)
            {
                SpawnEnemy(EnemyType.Average);
                _elapsedTimeForAverage = 0;
            }

            if (_elapsedTimeForStrong > _spawnTimeForStrongEnemy)
            {
                SpawnEnemy(EnemyType.Strong);
                _elapsedTimeForStrong = 0;
            }
        }
    }

    private void SpawnEnemy(EnemyType type)
    {
        var enemy = _enemyPools[(int) type].Pool.Get().GetComponent<EnemyController>();
         
        enemy.Initialize(this, false, _spawnObjectOfExpirience);
        enemy.transform.position = FindRandomPosition().position;
        _spawnedEnemies.Add(enemy);
    }


    public Transform FindRandomPosition()
    {
        var spawnVariant = Random.Range(0, 3);
        Transform vector = transform;
        if (spawnVariant == 0)
        {
            vector.position = new Vector3(
                0,
                0,
                Random.Range(0, 2) == 0 ? Random.Range(-42f, -40f) : Random.Range(14f, 16f));
        }
        else if (spawnVariant == 1)
        {
            vector.position = new Vector3(
                Random.Range(0, 2) == 0 ? Random.Range(20f, 22f) : Random.Range(-22f, -20f),
                0,
                0);
        }
        else
        {
            vector.position = new Vector3(
                Random.Range(0, 2) == 0 ? Random.Range(18f, 20f) : Random.Range(-20f, -18f),
                0,
                Random.Range(0, 2) == 0 ? Random.Range(-42f, -40f) : Random.Range(14f, 16f));
        }

        vector.position += _player.position;

        return vector;
    }
}