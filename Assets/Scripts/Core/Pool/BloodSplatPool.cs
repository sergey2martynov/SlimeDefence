using CodeBase.Core;
using DG.Tweening;
using UnityEngine;

public class BloodSplatPool : AbstractPool
{
    public override GameObject Get()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeSelf)
            {
                _pool[i].SetActive(true);
                DOTween.Sequence().AppendInterval(3f).OnComplete(() => Release(_pool[i]));
                return _pool[i];
            }
        }
            
        var poolObject = Instantiate(_poolObject, transform.position, Quaternion.identity, _parent);
        DOTween.Sequence().AppendInterval(3f).OnComplete(() => Release(poolObject));
        _pool.Add(poolObject);
        return poolObject;
    }
}
