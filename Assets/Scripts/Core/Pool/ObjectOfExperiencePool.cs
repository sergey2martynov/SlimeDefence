using System.Collections;
using System.Collections.Generic;
using CodeBase.Core;
using UnityEngine;

public class ObjectOfExperiencePool :AbstractPool
{
    public override GameObject CreateObject()
    {
        var poolObject = Instantiate(_poolObject, transform.position, Quaternion.identity, _parent);
        poolObject.GetComponent<ObjectOfExpirience>().Initialize(this);
        return poolObject;
    }
    
    
}
