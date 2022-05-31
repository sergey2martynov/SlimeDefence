using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using DG.Tweening;
using StaticData.Enemy;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private EnemySpawnIntervals _enemySpawnIntervals;
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private SpawnObjectOfExperience _spawnObjectOfExperience;
    [SerializeField] private List<EnemyPool> _enemyPools;
    [SerializeField] private List<AnimationCurve> _spawnIntervals;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private int _maxNumberOfEnemies;


    private float _currentTimeForWeakEnemy;
    private float _currentTimeForAverageEnemy;
    private float _currentTimeForStrongEnemy;

    private float _elapsedTimeForWeak;
    private float _elapsedTimeForAverage;
    private float _elapsedTimeForStrong;
    private float _currentTime;

    private List<Enemy> _spawnedEnemies;
    public List<Enemy> SpawnedEnemies => _spawnedEnemies;
    public List<EnemyPool> EnemyPools => _enemyPools;

    private void SetKeysCurve()
    {
        for (int i = 0; i < 3; i++)
        {
            _spawnIntervals[i] = AnimationCurve.Linear(0, _enemySpawnIntervals.FirstCurveValue[i],
                _stagesLevel.LevelDuration, _enemySpawnIntervals.LastCurveValue[i]);
        }
    }

    private void Start()
    {
        _currentTimeForWeakEnemy = _spawnIntervals[0].Evaluate(_elapsedTimeForWeak);
        _currentTimeForAverageEnemy = _spawnIntervals[1].Evaluate(_elapsedTimeForWeak);
        _currentTimeForStrongEnemy = _spawnIntervals[2].Evaluate(_elapsedTimeForWeak);
        _spawnedEnemies = new List<Enemy>();
        SetKeysCurve();
        _timeCounter.FinalStageBegun += RemoveAllEnemies;
    }

    private void OnDestroy()
    {
        _timeCounter.FinalStageBegun -= RemoveAllEnemies;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        _elapsedTimeForWeak += Time.deltaTime;
        _elapsedTimeForAverage += Time.deltaTime;
        _elapsedTimeForStrong += Time.deltaTime;

        if (_spawnedEnemies.Count < _maxNumberOfEnemies && !_timeCounter.IsFinalStageLevel)
        {
            if (_elapsedTimeForWeak > _currentTimeForWeakEnemy)
            {
                SpawnEnemy(EnemyType.Weak);
                _currentTimeForWeakEnemy = _spawnIntervals[0].Evaluate(_currentTime);
                _elapsedTimeForWeak = 0;
            }

            if (_elapsedTimeForAverage > _currentTimeForAverageEnemy)
            {
                SpawnEnemy(EnemyType.Average);
                _currentTimeForAverageEnemy = _spawnIntervals[1].Evaluate(_currentTime);
                _elapsedTimeForAverage = 0;
            }

            if (_elapsedTimeForStrong > _currentTimeForStrongEnemy)
            {
                SpawnEnemy(EnemyType.Strong);
                _currentTimeForStrongEnemy = _spawnIntervals[2].Evaluate(_currentTime);
                _elapsedTimeForStrong = 0;
            }
        }
    }

    private void SpawnEnemy(EnemyType type)
    {
        var enemy = _enemyPools[(int) type].Pool.Get().GetComponent<Enemy>();

        enemy.Initialize(_spawnObjectOfExperience);
        enemy.SetSpawnerEnemiesRef(this);
        _spawnedEnemies.Add(enemy);
    }

    private void RemoveAllEnemies()
    {
        var count = _spawnedEnemies.Count;

        DOTween.Sequence().AppendInterval(0.5f).OnComplete(() => { });

        for (int i = 0; i < count; i++)
        {
            _spawnedEnemies[i].gameObject.SetActive(false);
        }
    }
}