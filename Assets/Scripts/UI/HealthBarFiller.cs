using StaticData;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFiller : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private HealthLevels _healthLevels;

    private int _maxValue;

    private Color color = Color.green;

    private void Start()
    {
        _maxValue = _healthLevels.GetHealthParameters(0).Amount;
        _slider.fillRect.GetComponent<Image>().color = color;

        _slider.maxValue = _maxValue;
        _slider.value = _maxValue;
        _slider.minValue = 0;

        _health.MaxHealthChanged += UpdateMaxValue;
    }

    private void OnDestroy()
    {
        _health.MaxHealthChanged -= UpdateMaxValue;
    }

    private void Update()
    {
        if (_slider.value < 0) _slider.value = 0;
        if (_slider.value > _maxValue) _slider.value = _maxValue;
        _slider.value = _health.HealthPoint;
    }

    private void UpdateMaxValue(int maxHealth)
    {
        _slider.maxValue = maxHealth;
    }
}