using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class RailGunTarget : MonoBehaviour
{
    private Transform _target;

    private void Update()
    {
        if (_target != null)
            transform.position = _target.position;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
