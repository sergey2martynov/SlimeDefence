using System.Collections;
using System.Collections.Generic;
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
}
