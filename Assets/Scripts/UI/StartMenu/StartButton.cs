using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private FloatingJoystick _fixedJoystick;
    [SerializeField] private TimeCounter _timeCounter;

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
        Time.timeScale = 1;
        _startMenu.gameObject.SetActive(false);
        _fixedJoystick.gameObject.SetActive(true);
        _timeCounter.ResetElapsedTime();
    }
}
