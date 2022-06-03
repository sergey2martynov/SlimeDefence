using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveParameters
{
    [SerializeField] private int _numberOfWave;
    [SerializeField] private float _durationWave;
    [SerializeField] private List<EnemyType> _enemies;
    [SerializeField] private List<float> _initialSpawnRate;
    [SerializeField] private List<float> _lastSpawnRate;
    [SerializeField] private List<float> _timesSpawnMiniBoss;
    [SerializeField] private int _numberOfBosses;

    public int NumberOfWave => _numberOfWave;
    public float DurationWave => _durationWave;
    public List<EnemyType> Enemies => _enemies;
    public List<float> InitialSpawnRate => _initialSpawnRate;
    public List<float> LastSpawnRate => _lastSpawnRate;
    public List<float> TimesSpawnMiniBoss => _timesSpawnMiniBoss;
    public int NumberOfBosses => _numberOfBosses;

}
