using UnityEngine;
using UnityEngine.UI;

public class HealthBarFiller : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    
    private int maxValue;
    private Color color = Color.green;
	
    private void Start()
    {
        maxValue = _health.HealthPoint;
        _slider.fillRect.GetComponent<Image>().color = color;

        _slider.maxValue = maxValue;
        _slider.minValue = 0;
    }
	
    private void Update () 
    {
        if(_slider.value < 0) _slider.value = 0;
        if(_slider.value > maxValue) _slider.value = maxValue;
        _slider.value = _health.HealthPoint;
    }

}
