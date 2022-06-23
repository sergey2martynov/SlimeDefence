using UI.RestartMenu;
using UnityEngine;

public class DieDetector : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private RestartMenuDisabler _restartMenuDisabler;
    [SerializeField] private AudioSource _deathSound;

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
        _deathSound.Play();
        Time.timeScale = 0;
        _restartMenuDisabler.RestartMenuDisable(true);
    }
}
