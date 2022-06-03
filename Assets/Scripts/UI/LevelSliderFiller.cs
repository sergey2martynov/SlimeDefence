using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class LevelSliderFiller : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private ProgressController _progress;
    [SerializeField] private TimeCounter _timeCounter;

    private int _currentWave;
    

    private void Start()
    {
        MoveSlider();
        _timeCounter.ChangedWave += UpdateSlider;
    }

    private void OnDestroy()
    {
        _timeCounter.ChangedWave -= UpdateSlider;
    }

    private void MoveSlider()
    {
        _slider.DOValue(1, _stagesLevel.WaveParameters[_currentWave].DurationWave);
    }

    private void UpdateSlider()
    {
        _slider.value = 0;
        _currentWave++;
        _slider.DOValue(1, _stagesLevel.WaveParameters[_currentWave].DurationWave);
    }
    
}
