using System;
using Analytics;
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
    public event Action<int> SpawnBossTimeHasCome;
    public event Action ChangedWave;
    public event Action WeaponReceived;
    
    private void Start()
    {
        ProgressData.CurrentWave = _currentWave;
        Application.targetFrameRate = 60;
        _currentWaveDuration = _stagesLevel.GetWaveParameters(_currentWave).DurationWave;
        _numberWaveText.text = "WAVE " + (_currentWave + 1);
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
    }

    public void ResetElapsedTime()
    {
        _elapsedTime = 0;
    }

    public void UpdateWave()
    {
        EventSender.SendLevelFinish();
        
        if (_stagesLevel.GetWaveParameters(_currentWave).IsGetNewWeapon)
        {
            WeaponReceived?.Invoke();
        }
        _currentWave++;
        ProgressData.CurrentWave = _currentWave;
        ProgressData.LevelCount++;

        _elapsedTime = 0;

        if (_currentWave < _stagesLevel.WaveParameters.Count)
        {
            _currentWaveDuration = _stagesLevel.GetWaveParameters(_currentWave).DurationWave;
            ChangedWave?.Invoke();
            
            if (!_stagesLevel.GetWaveParameters(_currentWave - 1).IsGetNewWeapon)
            {
                EventSender.SendLevelStart();
            }
        }
        
        _isFinalStageWave = false;
        
        _numberWaveText.text = "WAVE" + (_currentWave + 1);
    }
}