using System.Collections;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; private set; }
    private float _lifeTime;
    private Vector3 _direction;
    private float _speed;
    private GunshotProjectilePool _pool;
    private int _penetrationCounter;

    public void Initialize(int damage, Vector3 direction, GunshotProjectilePool pool, Transform currentPos, float speed,
        int maxPenetration, float lifeTime = 4f)
    {
        var startPosOffset = new Vector3(0, 1, 0);
        
        _pool = pool;
        var transformProjectile = transform;
        Damage = damage;
        _direction = direction;
        _lifeTime = lifeTime;
        _speed = speed;
        transformProjectile.forward = direction;
        transformProjectile.position = currentPos.position + startPosOffset;
        _penetrationCounter = maxPenetration;

        StartCoroutine(DestoryOnTime());
    }

    private IEnumerator DestoryOnTime()
    {
        transform.localScale = Vector3.one;
        yield return new WaitForSecondsRealtime(_lifeTime);

        _pool.Release(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position += _direction.normalized * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemyController))
        {
            _penetrationCounter--;

            enemyController.Health.GetDamage(Damage);

            if (_penetrationCounter <= 0)
            {
                _pool.Release(gameObject);
            }
        }
    }
}