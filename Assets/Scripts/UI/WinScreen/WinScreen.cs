using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resultKills;
    [SerializeField] private KillCounter _killCounter;
    [SerializeField] private FloatingJoystick _joystick;

    public void ShowWinScreen(bool isActive)
    {
        gameObject.SetActive(isActive);

        if (isActive)
        {
            _resultKills.text = _killCounter.Counter + " kills";
            _joystick.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
}
