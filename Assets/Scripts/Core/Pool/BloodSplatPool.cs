using CodeBase.Core;
using DG.Tweening;
using UnityEngine;

public class BloodSplatPool : AbstractPool
{
    public override GameObject CreateObject()
    {
        var poolObject = Instantiate(_poolObject, transform.position, Quaternion.identity, _parent);
        DOTween.Sequence().AppendInterval(3f).OnComplete(() => Pool.Release(poolObject));
        return poolObject;
    }

    public override void ActionOnGet(GameObject poolObject)
    {
        if (poolObject == null || poolObject.activeSelf)
        {
            CreateObject();
            return;
        }
            
        poolObject.SetActive(true);
        DOTween.Sequence().AppendInterval(3f).OnComplete(() => Pool.Release(poolObject));
    }
}
