using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _joyStick;

        private bool _isPause;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (_isPause)
            {
                Time.timeScale = 1;
                _joyStick.SetActive(true);
                _isPause = !_isPause;
            }
            else
            {
                Time.timeScale = 0;
                _joyStick.SetActive(false);
                _isPause = !_isPause;
            }
        }
    }
}