using System;
using UnityEngine;
using UnityEngine.Pool;

public class GunshotProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _maxSize;
    [SerializeField] private Transform _parent;
    
    private IObjectPool<Projectile> _pool;
    public IObjectPool<Projectile> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Projectile>(CreateProjectile, (obj
                ) => obj.gameObject.SetActive(true), (obj) => obj.gameObject.SetActive(false), (obj
                ) => Destroy(obj), false, 10, _maxSize);
            }

            return _pool;
        }
    }
    private Projectile CreateProjectile()
    {
        var projectile = Instantiate(_projectile, transform.position, Quaternion.identity, _parent);
        return projectile;
    }


    
}