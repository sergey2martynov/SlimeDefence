using UnityEngine;

namespace UI.RestartMenu
{
    public class RestartMenuDisabler : MonoBehaviour
    {
        [SerializeField] private GameObject _restartMenu;
        [SerializeField] private FloatingJoystick _floatingJoystick;
    
        public void RestartMenuDisable(bool isActive)
        {
            _restartMenu.SetActive(isActive);
            if(isActive)
                _floatingJoystick.gameObject.SetActive(false);
        }
    }
}
