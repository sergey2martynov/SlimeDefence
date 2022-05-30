using System;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private StagesLevel _stagesLevel;
    private float _elapsedTime;
    private bool _isFinalStageLevel;

    public bool IsFinalStageLevel => _isFinalStageLevel;

    public event Action FinalStageBegun;

    public float ElapsedTime => _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

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