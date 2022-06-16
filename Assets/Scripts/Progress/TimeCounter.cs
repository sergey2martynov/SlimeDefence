using System;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private TextMeshProUGUI _numberWaveText;

    private float _currentWaveDuration;
    private float _elapsedTime;
    private bool _isFinalStageWave;
    private int _currentWave;
    private int _currentMiniBoss;

    public int CurrentWave => _currentWave;
    public event Action FinalStageBegun;
    public event Action<int> SpawnBossTimeHasCome;
    public event Action ChangedWave;
    public event Action WeaponReceived;
    
    private void Start()
    {
        _currentWaveDuration = _stagesLevel.GetWaveParameters(_currentWave).DurationWave;
        _numberWaveText.text = (_currentWave + 1).ToString();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _currentWaveDuration && _currentWave < _stagesLevel.WaveParameters.Count && !_isFinalStageWave)
        {
            _isFinalStageWave = true;
            
            var numberOfBosses = _stagesLevel.GetWaveParameters(_currentWave).NumberOfBosses;
            
            SpawnBossTimeHasCome?.Invoke(numberOfBosses);
            
            if (numberOfBosses == 0)
            {
                UpdateWave();
            }
        }
        else if (_currentWave == _stagesLevel.WaveParameters.Count && !_isFinalStageWave)
        {
            _isFinalStageWave = true;
            FinalStageBegun?.Invoke();
        }
    }

    public void ResetElapsedTime()
    {
        _elapsedTime = 0;
    }

    public void UpdateWave()
    {
        if (_stagesLevel.GetWaveParameters(_currentWave).IsGetNewWeapon)
        {
            WeaponReceived?.Invoke();
        }
        _currentWave++;
        _elapsedTime = 0;
                
        if (_currentWave < _stagesLevel.WaveParameters.Count)
        {
            _currentWaveDuration = _stagesLevel.GetWaveParameters(_currentWave).DurationWave;
            ChangedWave?.Invoke();
        }
        
        _isFinalStageWave = false;
        
        _numberWaveText.text = (_currentWave + 1).ToString();
    }
}