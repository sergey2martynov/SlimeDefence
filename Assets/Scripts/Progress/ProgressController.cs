using System;
using StaticData;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private LevelState _levelState;
    [SerializeField] private ExperienceBar _experienceBar;

    private int _currentLevel;
    private int _expirience;

    public int Expirience => _expirience;

    public int MaxCurrentExperience => _levelState.Experience[_currentLevel - 1];

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level_Player"))
        {
            _expirience = 0;
            _currentLevel = 1;
        }
        else
        {
            LoadProgress();
        }
    }

    public void GetExperience(int experience)
    {
        _expirience += experience;

        if (_expirience > _levelState.Experience[_currentLevel - 1])
        {
            _expirience = 0;
            _currentLevel++;
            _experienceBar.SetMaxValue();
            PlayerData.Level = _currentLevel;
        }

        _experienceBar.SetSlider(_expirience);
    }

    private void LoadProgress()
    {
        _expirience = PlayerData.Experience;
        _currentLevel = PlayerData.Level;
    }
}