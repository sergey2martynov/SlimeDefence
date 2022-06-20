using CodeBase.Core.Character.Enemy;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthBarFiller : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Health _health;
    [SerializeField] private Enemy _enemy;
    private int _startHealth;
    private Camera _camera;

    private void Start()
    {
        _health.HealthChanged += OnHealthChanged;
        _startHealth = _health.HealthPoint;
        _camera = _enemy.Camera;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    public void ReturnHealthBarValue()
    {
        _healthBar.fillAmount = _health.HealthPoint;
    }

    private void OnHealthChanged(float damage)
    {
        _healthBar.fillAmount -= damage / _startHealth;
    }
    
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0,180,0);
    }
}
