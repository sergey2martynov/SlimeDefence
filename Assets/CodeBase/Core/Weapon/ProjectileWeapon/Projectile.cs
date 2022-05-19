using System.Collections;
using CodeBase.Core.Character.Enemy;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; private set; }
    private float _lifeTime;
    private Vector3 _direction;
    private float _speed;
    private GunshotProjectilePool _pool;

    public void Initialize(int damage,Vector3 direction, GunshotProjectilePool pool,Transform currentPos, float lifeTime = 1f, float speed = 1f)
    {
        var transformProjectile = transform;
        Damage = damage;
        _direction = direction;
        _lifeTime = lifeTime;
        _speed = speed;
        transformProjectile.forward = direction;
        _pool = pool;
        transformProjectile.position = currentPos.position;
        StartCoroutine(DestoryOnTime());
    }

    private IEnumerator DestoryOnTime()
    {
        transform.localScale = Vector3.one;
        yield return new WaitForSecondsRealtime(_lifeTime);
        transform.DOScale(new Vector3(), 2f).onComplete+= () =>
        {
            _pool.Pool.Release(this);
        };
    }

    private void FixedUpdate()
    {
        transform.position += _direction.normalized * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
        
        if (enemyController != null)
        {
            enemyController.Health.GetDamage(Damage);
            _pool.Pool.Release(this);
        }
    }
}
