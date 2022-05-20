using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private ProgressController _progressController;
    
    private void Start()
    {
        SetMaxValue();
        _slider.value = _progressController.Expirience;
    }

    public void SetMaxValue()
    {
        _slider.maxValue = _progressController.MaxCurrentExperience;
    }

    public void SetSlider(int experience)
    {
        _slider.value = experience;
    }
}