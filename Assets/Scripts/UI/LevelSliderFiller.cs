using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSliderFiller : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private StagesLevel _stagesLevel;

    private void Start()
    {
        _slider.DOValue(1, _stagesLevel.LevelDuration);
    }
}
