using UnityEngine;

namespace UI.RestartMenu
{
    public class RestartMenuDisabler : MonoBehaviour
    {
        [SerializeField] private GameObject _restartMenu;
    
        public void RestartMenuDisable(bool isActive)
        {
            _restartMenu.SetActive(isActive);
        }
    }
}
