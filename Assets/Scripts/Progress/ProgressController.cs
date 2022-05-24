using CodeBase.Core.Character.Player;
using Core.Character.Player;
using StaticData;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private PauseController _pauseController;
    [SerializeField] private LevelUpMenuDisabler _levelUpMenuDisabler;
    [SerializeField] private LevelState _levelState;
    [SerializeField] private ExperienceBar _experienceBar;
    [SerializeField] private PlayerController _playerController;
    
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

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level_Player"))
        {
            _expirience = 0;
            _playerController.AddedPlayerLevel();
        }
        else
        {
            LoadProgress();
        }
    }

    public void GetExperience(int experience)
    {
        _expirience += experience;

        if (_expirience > MaxCurrentExperience)
        {
            _expirience = 0;
            _playerController.AddedPlayerLevel();
            _experienceBar.SetMaxValue();
            PlayerData.Level = _playerController.CurrentLevel;
            _levelUpMenuDisabler.LevelUpMenuDisable(true);
            _pauseController.Pause(true);
        }

        _experienceBar.SetSlider(_expirience);
    }

    private void LoadProgress()
    {
        // _expirience = PlayerData.Experience;
        // _playerController.CurrentLevel = PlayerData.Level;
    }
}