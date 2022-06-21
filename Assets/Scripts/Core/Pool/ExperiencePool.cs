using CodeBase.Core;
using Core.Expirience;
using UnityEngine;

public class ExperiencePool :AbstractPool
{
    [SerializeField] private Transform _player;
    public override GameObject Get()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeSelf)
            {
                _pool[i].SetActive(true);
                return _pool[i];
            }
        }
            
        var poolObject = Instantiate(_poolObject, transform.position, Quaternion.identity, _parent);
        poolObject.GetComponent<Experience>().Initialize(this, _player);
        _pool.Add(poolObject);
        return poolObject;
    }
}
