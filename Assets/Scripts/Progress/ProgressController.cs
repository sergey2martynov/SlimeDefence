using System;
using Core.Character.Player;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private LevelState _levelState;
    [SerializeField] private ExperienceBar _experienceBar;
    [SerializeField] private PlayerController _playerController;

    public event Action PlayerLeveledUp;
    
    private int _expirience;

    public int Expirience => _expirience;

    public int MaxCurrentExperience
    {
        get
        {
            if (_playerController.CurrentLevel == 0)
            {
                return _levelState.Experience[0];
            }
            
            return _levelState.Experience[_playerController.CurrentLevel - 1];
        }
    }

    public void GetExperience(int experience)
    {
        _expirience += experience;

        if (_expirience >= MaxCurrentExperience)
        {
            PlayerLeveledUp?.Invoke();
            _expirience = 0;
            _playerController.AddedPlayerLevel();
            _experienceBar.SetMaxValue();
        }

        _experienceBar.SetSlider(_expirience);
    }
}