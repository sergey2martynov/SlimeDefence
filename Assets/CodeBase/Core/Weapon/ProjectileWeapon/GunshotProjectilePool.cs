using System;
using UnityEngine;
using UnityEngine.Pool;

public class GunshotProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _maxSize;


    private Projectile CreateProjectile()
    {
        var transformObject = transform;
        var projectile = Instantiate(_projectile, transformObject.position, Quaternion.identity, transformObject);
        return projectile;
    }

    private IObjectPool<Projectile> _pool;

    public IObjectPool<Projectile> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Projectile>(CreateProjectile, (obj
                ) => obj.gameObject.SetActive(true), (obj) => obj.gameObject.SetActive(false), (obj
                ) => Destroy(obj), false, 1, _maxSize);
            }

            return _pool;
        }
    }
}