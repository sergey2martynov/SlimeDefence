using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Core
{
    public abstract class AbstractPool : MonoBehaviour
    {
        [SerializeField] protected GameObject _poolObject;
        [SerializeField] private int _maxSize;
        [SerializeField] protected Transform _parent;
        [SerializeField] private int _defaultCapacity;

        private IObjectPool<GameObject> _pool;

        public IObjectPool<GameObject> Pool
        {
            get
            {
                if (_pool == null)
                {
                    _pool = new ObjectPool<GameObject>(CreateObject, ActionOnGet, ActionOnRelease, (obj
                    ) => Destroy(obj), false, _defaultCapacity, _maxSize);
                }

                return _pool;
            }
        }

        public virtual GameObject CreateObject()
        {
            var poolObject = Instantiate(_poolObject, transform.position, Quaternion.identity, _parent);
            return poolObject;
        }

        public virtual void ActionOnGet(GameObject poolObject)
        {
            // if (poolObject.activeSelf)
            //     CreateObject();
            
            poolObject.SetActive(true);
        }

        public virtual void ActionOnRelease(GameObject poolObject)
        {
            poolObject.SetActive(false);
        }
    }
}