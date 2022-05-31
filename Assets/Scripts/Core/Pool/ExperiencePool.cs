using CodeBase.Core;
using UnityEngine;

public class ExperiencePool :AbstractPool
{
    public override GameObject CreateObject()
    {
        var poolObject = Instantiate(_poolObject, transform.position, Quaternion.identity, _parent);
        poolObject.GetComponent<Experience>().Initialize(this);
        return poolObject;
    }
}
