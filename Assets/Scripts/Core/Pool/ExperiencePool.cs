using CodeBase.Core;
using Core.Expirience;
using UnityEngine;

public class ExperiencePool :AbstractPool
{
    [SerializeField] private Transform _player;
    public override GameObject CreateObject()
    {
        var poolObject = Instantiate(_poolObject, transform.position, Quaternion.identity, _parent);
        poolObject.GetComponent<Experience>().Initialize(this, _player);
        return poolObject;
    }
}
