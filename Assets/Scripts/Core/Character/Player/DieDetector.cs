using UI.RestartMenu;
using UnityEngine;

public class DieDetector : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private RestartMenuDisabler _restartMenuDisabler;

    private void Start()
    {
        _health.HealthIsOver += Die;
    }

    private void OnDestroy()
    {
        _health.HealthIsOver -= Die;
    }

    private void Die()
    {
        Time.timeScale = 0;
        _restartMenuDisabler.RestartMenuDisable(true);
    }
}
