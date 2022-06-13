using CodeBase.Core.Character.Enemy;
using DG.Tweening;
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
            enemyController.MeshRenderer.material.color = Color.white;
            
            DOTween.Sequence().AppendInterval(0.07f).OnComplete(() =>
            {
                enemyController.ReturnColor();
                enemyController.Health.GetDamage(_damage);
            });
        }
    }
}
