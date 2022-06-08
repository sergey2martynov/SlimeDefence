using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using Core.Environment;
using DG.Tweening;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private KillCounter _killCounter;
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private List<EnemyPool> _enemyPools;
    [SerializeField] private List<AnimationCurve> _spawnIntervals;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private ExperiencePool _experiencePool;
    [SerializeField] private HealthBox _healthBox;
    [SerializeField] private Transform _healthBoxesParent;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _maxNumberOfEnemies;
    
    private float _currentTime;
    private int _currentWave;
    private WaveParameters _currentWaveParameters;
    private int _numberOfEnemies;
    private bool _isStageFinalBoss;

    private List<Enemy> _spawnedEnemies;
    private List<float> _elapsedTimes;
    private List<float> _currentSpawnRate;
    public List<Enemy> SpawnedEnemies => _spawnedEnemies;
    public List<EnemyPool> EnemyPools => _enemyPools;

    private void Start()
    {
        _currentWaveParameters = _stagesLevel.GetWaveParameters(_currentWave);
        _numberOfEnemies = _currentWaveParameters.Enemies.Count;
        _spawnedEnemies = new List<Enemy>();
        _elapsedTimes = new List<float>{0,0,0,0};
        _currentSpawnRate = new List<float>{0,0,0,0};
        _timeCounter.FinalStageBegun += RemoveAllEnemies;
        _timeCounter.ChangedWave += UpdateWaveParameters;
        
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            _spawnIntervals[i] = AnimationCurve.Linear(0, _currentWaveParameters.InitialSpawnRate[i],
                _currentWaveParameters.DurationWave, _currentWaveParameters.LastSpawnRate[i]);
            _elapsedTimes[i] = 0;
            _currentSpawnRate[i] = 1;
        }
    }

    private void OnDestroy()
    {
        _timeCounter.FinalStageBegun -= RemoveAllEnemies;
        _timeCounter.ChangedWave -= UpdateWaveParameters;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (!_isStageFinalBoss)
        {
            for (int i = 0; i < _numberOfEnemies; i++)
            {
                _elapsedTimes[i] += Time.deltaTime;
                _currentSpawnRate[i] = _spawnIntervals[i].Evaluate(_currentTime);
            }

            for (int i = 0; i < _currentWaveParameters.Enemies.Count; i++)
            {
                if (_spawnedEnemies.Count < _maxNumberOfEnemies && _elapsedTimes[i] > _currentSpawnRate[i])
                {
                    SpawnEnemy(_currentWaveParameters.Enemies[i]);
                    _elapsedTimes[i] = 0;
                    _currentSpawnRate[i] = _spawnIntervals[i].Evaluate(_currentTime);
                }
            }
        }
    }

    private void UpdateWaveParameters()
    {
        _currentWave++;
        _currentWaveParameters = _stagesLevel.GetWaveParameters(_currentWave);
        _numberOfEnemies = _currentWaveParameters.Enemies.Count;
        
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            _spawnIntervals[i] = AnimationCurve.Linear(0, _currentWaveParameters.InitialSpawnRate[i],
                _currentWaveParameters.DurationWave, _currentWaveParameters.LastSpawnRate[i]);
            _elapsedTimes[i] = 0;
            _currentSpawnRate[i] = 1;
        }
        _currentTime = 0;
    }

    private void SpawnEnemy(EnemyType type)
    {
        var enemy = _enemyPools[(int) type].Pool.Get().GetComponent<Enemy>();

        enemy.Initialize(_killCounter, _winScreen, _experiencePool, _healthBox, _healthBoxesParent, _camera);
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