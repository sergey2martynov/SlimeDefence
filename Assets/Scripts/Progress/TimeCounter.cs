using System;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private StagesLevel _stagesLevel;
    private float _elapsedTime;
    private bool _isFinalStageLevel;
    private int _currentStage;

    public bool IsFinalStageLevel => _isFinalStageLevel;

    public event Action FinalStageBegun;
    public event Action IntermediateStageBegun;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        
        if (_elapsedTime > _stagesLevel.TimesOfSpawnBosses[_currentStage] && _currentStage < _stagesLevel.TimesOfSpawnBosses.Count - 1)
        {
            IntermediateStageBegun?.Invoke();
            _currentStage++;
        }

        if (_elapsedTime > _stagesLevel.LevelDuration && _isFinalStageLevel == false)
        {
            FinalStageBegun?.Invoke();
            _isFinalStageLevel = true;
        }
    }

    public void ResetElapsedTime()
    {
        _elapsedTime = 0;
    }
}