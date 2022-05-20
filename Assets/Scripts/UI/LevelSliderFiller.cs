using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSliderFiller : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _levelTime;

    private void Start()
    {
        _slider.DOValue(1, _levelTime);
    }
    
    private void Update () 
    {
        // if(_slider.value < 0) _slider.value = 0;
        // if(_slider.value > maxValue) _slider.value = maxValue;
        // _slider.value = _health.HealthPoint;
    }
}
