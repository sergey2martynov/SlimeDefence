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

    public void Initialize(int damage,Vector3 direction, float lifeTime = 1f, float speed = 1f)
    {
        Damage = damage;
        _direction = direction;
        _lifeTime = lifeTime;
        _speed = speed;
        transform.forward = direction;
        StartCoroutine(DestoryOnTime());
    }

    private IEnumerator DestoryOnTime()
    {
        yield return new WaitForSecondsRealtime(_lifeTime);
        transform.DOScale(new Vector3(), 0.1f).onComplete+= () =>
        {
            gameObject.SetActive(false);
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
            gameObject.SetActive(false);
        }
    }
}
