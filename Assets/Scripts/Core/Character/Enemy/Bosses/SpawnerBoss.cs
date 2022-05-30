using System;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    [SerializeField] private EnemyController _miniBoss;
    [SerializeField] private EnemyController _finalBoss;
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _parent;
    [SerializeField] private SpawnObjectOfExperience _spawnObjectOfExperience;
    [SerializeField] private TimeCounter _timeCounter;

    private int _currentIndex;
    private Vector3 _offsetPositionMiniBoss;
    private Vector3 _offsetPositionFinalBoss;
    private bool _isFinalBossSpawned;


    private void Start()
    {
        _offsetPositionMiniBoss = new Vector3(0, 0, -40);
        _offsetPositionFinalBoss = new Vector3(0, 0, -20);
        _timeCounter.FinalStageBegun += SpawnFinalBoss;
    }

    private void OnDestroy()
    {
        _timeCounter.FinalStageBegun -= SpawnFinalBoss;
    }

    private void Update()
    {
        if (_stagesLevel.TimesOfSpawnBosses[_currentIndex] < _timeCounter.ElapsedTime && !_isFinalBossSpawned)
        {
            var miniBoss = Instantiate(_miniBoss, _player.position + _offsetPositionMiniBoss, Quaternion.identity, _parent);
            miniBoss.Initialize(_spawnObjectOfExperience);
            _currentIndex++;
        }
    }

    private void SpawnFinalBoss()
    {
        _isFinalBossSpawned = true;
        var finalBoss = Instantiate(_finalBoss, _player.position + _offsetPositionFinalBoss, Quaternion.identity, _parent);
        finalBoss.Initialize(_spawnObjectOfExperience);
    }
}