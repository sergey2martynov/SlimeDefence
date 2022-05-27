using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private SpawnObjectOfExperience _spawnObjectOfExperience;
    [SerializeField] private List<EnemyPool> _enemyPools;
    [SerializeField] private int _maxNumberOfEnemies;
    [SerializeField] private Transform _player;
    [SerializeField] private float _currentTimeForWeakEnemy;
    [SerializeField] private float _currentTimeForAverageEnemy;
    [SerializeField] private float _currentTimeForStrongEnemy;
    [SerializeField] private AnimationCurve _spawnIntervalWeakEnemy;
    [SerializeField] private AnimationCurve _spawnIntervalAverageEnemy;
    [SerializeField] private AnimationCurve _spawnIntervalStrongEnemy;

    private float _elapsedTimeForWeak;
    private float _elapsedTimeForAverage;
    private float _elapsedTimeForStrong;
    private float _currentTime;

    private List<EnemyController> _spawnedEnemies;
    public List<EnemyController> SpawnedEnemies => _spawnedEnemies;
    public List<EnemyPool> EnemyPools => _enemyPools;

    private void Start()
    {
        _currentTimeForWeakEnemy = _spawnIntervalWeakEnemy.Evaluate(_elapsedTimeForWeak);
        _currentTimeForAverageEnemy = _spawnIntervalAverageEnemy.Evaluate(_elapsedTimeForWeak);
        _currentTimeForStrongEnemy = _spawnIntervalStrongEnemy.Evaluate(_elapsedTimeForWeak);
        _spawnedEnemies = new List<EnemyController>();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        _elapsedTimeForWeak += Time.deltaTime;
        _elapsedTimeForAverage += Time.deltaTime;
        _elapsedTimeForStrong += Time.deltaTime;

        if (_spawnedEnemies.Count < _maxNumberOfEnemies)
        {
            if (_elapsedTimeForWeak > _currentTimeForWeakEnemy)
            {
                SpawnEnemy(EnemyType.Weak);
                _currentTimeForWeakEnemy = _spawnIntervalWeakEnemy.Evaluate(_currentTime);
                _elapsedTimeForWeak = 0;
            }

            if (_elapsedTimeForAverage > _currentTimeForAverageEnemy)
            {
                SpawnEnemy(EnemyType.Average);
                _currentTimeForAverageEnemy = _spawnIntervalAverageEnemy.Evaluate(_currentTime);
                _elapsedTimeForAverage = 0;
            }

            if (_elapsedTimeForStrong > _currentTimeForStrongEnemy)
            {
                SpawnEnemy(EnemyType.Strong);
                _currentTimeForStrongEnemy = _spawnIntervalStrongEnemy.Evaluate(_currentTime);
                _elapsedTimeForStrong = 0;
            }
        }
    }

    private void SpawnEnemy(EnemyType type)
    {
        var enemy = _enemyPools[(int) type].Pool.Get().GetComponent<EnemyController>();
         
        enemy.Initialize(this, false, _spawnObjectOfExperience);
        enemy.transform.position = RandomPositionFinder.FindRandomPosition(transform, _player, -41, 15, -21, 21).position;
        _spawnedEnemies.Add(enemy);
    }
}