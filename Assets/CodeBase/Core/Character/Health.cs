using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _healthPoint;

    public event Action HealthIsOver;

    public void GetDamage(int damageTaken)
    {
        _healthPoint -= damageTaken;
        
        if (_healthPoint < 0)
        {
            HealthIsOver?.Invoke();
        }
    }
}
