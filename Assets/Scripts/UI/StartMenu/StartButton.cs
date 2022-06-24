using Analytics;
using UI.WeaponsPanel;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private FloatingJoystick _fixedJoystick;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private EnemyHealthBarFiller _healthBar;
    [SerializeField] private LevelSliderFiller _levelSliderFiller;
    [SerializeField] private KillCounter _killCounter;
    [SerializeField] private GameObject _playerLevel;
    [SerializeField] private WeaponsPanel _weaponsPanel;
    [SerializeField] private ExperienceBar _experienceBar;
    [SerializeField] private SoundPlayer _soundPlayer;

    private float _elapsedTime;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _soundPlayer.ButtonSoundPlay();
        Time.timeScale = 1;
        EventSender.SendLevelStart();
        _startMenu.gameObject.SetActive(false);
        _fixedJoystick.gameObject.SetActive(true);
        _timeCounter.ResetElapsedTime();
        _healthBar.gameObject.SetActive(true);
        _levelSliderFiller.gameObject.SetActive(true);
        _killCounter.gameObject.SetActive(true);
        _playerLevel.SetActive(true);
        _weaponsPanel.gameObject.SetActive(true);
        _experienceBar.gameObject.SetActive(true);
    }
}
