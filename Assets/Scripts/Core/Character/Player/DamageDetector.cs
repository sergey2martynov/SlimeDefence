using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    [SerializeField] private Health _health;

    private float _elapsedTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemyController))
        {
            _health.GetDamage(enemyController.Damage);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > 4 && other.gameObject.TryGetComponent(out Enemy enemyController))
        {
            _health.GetDamage(enemyController.Damage);
            _elapsedTime = 0;
        }
    }
}
