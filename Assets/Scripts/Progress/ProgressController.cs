using System;
using Core.Character.Player;
using DG.Tweening;
using StaticData.Player;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private PlayerLevelStages _levelState;
    [SerializeField] private ExperienceBar _experienceBar;
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem _particle;
    
    private int _expirience;
    public int Experience => _expirience;
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
            if(!_particle.gameObject.activeSelf)
                _particle.gameObject.SetActive(true);
            
            _particle.Play();
            _expirience = 0;
            _player.AddedPlayerLevel();
            _experienceBar.SetMaxValue();

            DOTween.Sequence().AppendInterval(1f).OnComplete(() =>
            {
                PlayerLeveledUp?.Invoke();
            });
        }

        _experienceBar.SetSlider(_expirience);
    }
}