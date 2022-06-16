using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class SunStrikeProjectile : MonoBehaviour
{
    private int _damage;
    
    public void Initialize(int damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemyController))
        {
            enemyController.Health.GetDamage(_damage);
        }
    }
}
