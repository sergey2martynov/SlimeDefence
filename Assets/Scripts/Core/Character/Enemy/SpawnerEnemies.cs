using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private KillCounter _killCounter;
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private List<EnemyPool> _enemyPools;
    [SerializeField] private List<AnimationCurve> _spawnIntervals;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private ExperiencePool _experiencePool;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _maxNumberOfEnemies;
    [SerializeField] private int _lethalDamage = 150;
    [SerializeField] private SpawnerBoss _spawnerBoss;
    [SerializeField] private BloodSplatPool _bloodSplatPool;
    [SerializeField] private Image _flashImage;

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

    private void Awake()
    {
        _currentWaveParameters = _stagesLevel.GetWaveParameters(_currentWave);
        _numberOfEnemies = _currentWaveParameters.Enemies.Count;
        _spawnedEnemies = new List<Enemy>();
        _elapsedTimes = new List<float> {0, 0, 0, 0, 0, 0};
        _currentSpawnRate = new List<float> {0, 0, 0, 0, 0, 0};
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
        var enemy = _enemyPools[(int) type].Get().GetComponent<Enemy>();

        enemy.Initialize(_killCounter, _experiencePool, _camera, _spawnerBoss, _timeCounter, _bloodSplatPool, 1, this);
        enemy.SetSpawnerEnemiesRef(this);
        _spawnedEnemies.Add(enemy);
        enemy.transform.position = FindSpawnRandomPosition();
        enemy.GetComponent<EnemyMovementInput>().MoveEnemy();
    }

    public void RemoveAllEnemies()
    {
        var count = _spawnedEnemies.Count;

        DOTween.ToAlpha(() => _flashImage.color, x => _flashImage.color = x, 30, 0.5f).OnComplete(() =>
        {
            DOTween.ToAlpha(() => _flashImage.color, x => _flashImage.color = x, 0, 1.5f).OnComplete(() =>
            {
                _flashImage.gameObject.SetActive(false);
                
                DOTween.Sequence().AppendInterval(0.1f).OnComplete((() =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        _spawnedEnemies[0].Health.GetDamage(_lethalDamage);
                    }
                }));
            });
        });
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