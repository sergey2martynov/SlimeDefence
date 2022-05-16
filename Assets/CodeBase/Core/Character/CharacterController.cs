using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    
    [SerializeField] private int _healthPoint;
    [SerializeField] private int _numberOfProjectiles;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _effectRadius;
    [SerializeField] private float _pickUpRadius;
    [SerializeField] private float _defence;

    public float GetSpeed()
    {
        return _speed;
    }
}
