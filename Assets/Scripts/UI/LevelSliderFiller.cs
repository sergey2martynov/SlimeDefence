using UnityEngine;
using UnityEngine.UI;

public class LevelSliderFiller : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private StagesLevel _stagesLevel;
    [SerializeField] private TimeCounter _timeCounter;

    private int _currentWave;
    
    private void Start()
    {
        _slider.maxValue = _stagesLevel.WaveParameters[_currentWave].DurationWave;
        _timeCounter.ChangedWave += UpdateSlider;
    }
    
    private void OnDestroy()
    {
        _timeCounter.ChangedWave -= UpdateSlider;
    }
    
    private void Update()
    {
        _slider.value += Time.deltaTime;
    }
    
    private void UpdateSlider()
    {
        _slider.value = 0;
        _currentWave++;
        _slider.maxValue = _stagesLevel.WaveParameters[_currentWave].DurationWave;
    }
}
