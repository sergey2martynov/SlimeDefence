using System;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private StagesLevel _stagesLevel;

    private float _currentWaveDuration;
    private float _elapsedTime;
    private bool _isFinalStageLevel;
    private int _currentWave;
    private int _currentMiniBoss;

    public bool IsFinalStageLevel => _isFinalStageLevel;

    public event Action FinalStageBegun;

    public event Action SpawnMiniBossTimeHasCome;
    public event Action ChangedWave;

    private void Start()
    {
        _currentWaveDuration = _stagesLevel.GetWaveParameters(_currentWave).DurationWave;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _currentWaveDuration && _currentWave < _stagesLevel.WaveParameters.Count)
        {
            _elapsedTime = 0;
            _currentMiniBoss = 0;
            _currentWave++;
            
            if (_currentWave < _stagesLevel.WaveParameters.Count)
            {
                _currentWaveDuration = _stagesLevel.GetWaveParameters(_currentWave).DurationWave;
                ChangedWave?.Invoke();
            }
        }
        else if (_currentWave == _stagesLevel.WaveParameters.Count && !_isFinalStageLevel)
        {
            _isFinalStageLevel = true;
            FinalStageBegun?.Invoke();
        }

        if (_currentWave < _stagesLevel.WaveParameters.Count &&
            _currentMiniBoss < _stagesLevel.GetWaveParameters(_currentWave).TimesSpawnMiniBoss.Count && _elapsedTime >
            _stagesLevel.GetWaveParameters(_currentWave).TimesSpawnMiniBoss[_currentMiniBoss])
        {
            _currentMiniBoss++;
            SpawnMiniBossTimeHasCome?.Invoke();
        }
    }

    public void ResetElapsedTime()
    {
        _elapsedTime = 0;
    }
}