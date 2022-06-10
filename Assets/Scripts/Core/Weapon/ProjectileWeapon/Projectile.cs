using System.Collections;
using CodeBase.Core;
using CodeBase.Core.Character.Enemy;
using Core.Environment;
using Core.Environment.Chest;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; private set; }
    private float _lifeTime;
    private Vector3 _direction;
    private float _speed;
    private GunshotProjectilePool _pool;

    public void Initialize(int damage,Vector3 direction, GunshotProjectilePool pool,Transform currentPos,  float speed, float lifeTime = 10f)
    {
        var startPosOffset = new Vector3(0, 1, 0);
        
        var transformProjectile = transform;
        Damage = damage;
        _direction = direction;
        _lifeTime = lifeTime;
        _speed = speed;
        transformProjectile.forward = direction;
        _pool = pool;
        transformProjectile.position = currentPos.position + startPosOffset;
        
        StartCoroutine(DestoryOnTime());
    }

    private IEnumerator DestoryOnTime()
    {
        transform.localScale = Vector3.one;
        yield return new WaitForSecondsRealtime(_lifeTime);
        
            _pool.Pool.Release(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position += _direction.normalized * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemyController))
        {
            enemyController.Health.GetDamage(Damage);
            _pool.Pool.Release(gameObject);
        }
        
        if (other.gameObject.TryGetComponent(out Chest chest))
        {
            chest.DestroyChest();
            _pool.Pool.Release(gameObject);
        }

        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _pool.Pool.Release(gameObject);
        }
    }
}
