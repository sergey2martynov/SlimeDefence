using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Core
{
    public abstract class AbstractPool : MonoBehaviour
    {
        [SerializeField] protected GameObject _poolObject;
        [SerializeField] protected Transform _parent;

        protected List<GameObject> _pool;

        private void Start()
        {
            _pool = new List<GameObject>();
        }

        public virtual GameObject Get()
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
            _pool.Add(poolObject);
            return poolObject;
        }

        public virtual void Release(GameObject poolObject)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (poolObject == _pool[i])
                {
                    poolObject.SetActive(false);
                    return;
                }
            }
        }
    }
}