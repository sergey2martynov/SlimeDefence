using System;
using Core.Character.Player;
using StaticData.Player;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private PlayerLevelStages _levelState;
    [SerializeField] private ExperienceBar _experienceBar;
    [SerializeField] private Player _player;
    
    private int _expirience;
    private int _waveNumber;
    public int Experience => _expirience;
    public int WaveNumber => _waveNumber;
    public event Action PlayerLeveledUp;

    public int MaxCurrentExperience
    {
        get
        {
            if (_player.CurrentLevel == 0)
            {
                return _levelState.Experience[0];
            }
            
            return _levelState.Experience[_player.CurrentLevel - 1];
        }
    }

    public void GetExperience(int experience)
    {
        _expirience += experience;

        if (_expirience >= MaxCurrentExperience)
        {
            PlayerLeveledUp?.Invoke();
            _expirience = 0;
            _player.AddedPlayerLevel();
            _experienceBar.SetMaxValue();
        }

        _experienceBar.SetSlider(_expirience);
    }

    public void IncreaseWaveNumber()
    {
        _waveNumber++;
    }
}