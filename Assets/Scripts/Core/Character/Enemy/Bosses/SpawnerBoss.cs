using System;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    [SerializeField] private Enemy _miniBoss;
    [SerializeField] private Enemy _finalBoss;
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _parent;
    [SerializeField] private SpawnObjectOfExperience _spawnObjectOfExperience;
    [SerializeField] private KillCounter _killCounter;
    [SerializeField] private TimeCounter _timeCounter;

    private int _currentIndex;
    private Vector3 _offsetPositionMiniBoss;
    private Vector3 _offsetPositionFinalBoss;
    
    private void Start()
    {
        _offsetPositionMiniBoss = new Vector3(0, 0, -40);
        _offsetPositionFinalBoss = new Vector3(0, 0, -20);
        _timeCounter.FinalStageBegun += SpawnFinalBoss;
        _timeCounter.IntermediateStageBegun += SpawnMiniBoss;
    }

    private void OnDestroy()
    {
        _timeCounter.FinalStageBegun -= SpawnFinalBoss;
        _timeCounter.IntermediateStageBegun -= SpawnMiniBoss;
    }

    private void SpawnBoss(Enemy enemy, Vector3 offset)
    {
        var finalBoss = Instantiate(enemy, _player.position + offset, Quaternion.identity, _parent);
        finalBoss.Initialize(_spawnObjectOfExperience, _killCounter);
    }

    private void SpawnFinalBoss()
    {
        SpawnBoss(_finalBoss, _offsetPositionFinalBoss);
    }
    
    private void SpawnMiniBoss()
    {
        SpawnBoss(_miniBoss, _offsetPositionMiniBoss);
    }
}