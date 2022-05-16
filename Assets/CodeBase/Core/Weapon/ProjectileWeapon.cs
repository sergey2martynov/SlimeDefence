using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Collider _gunCollider;

    private List<Collider> _colliders;

    private void OnCollisionEnter(Collision collision)
    {
        _colliders.Add(collision.collider);
    }

    private void OnCollisionExit(Collision other)
    {
        foreach (var collider in _colliders)
        {
            if (other.collider == collider)
            {
                _colliders.Remove(other.collider);
            }
        }
    }

    private void FindNearbyEnemy()
    {
        float minDistance;
        
        foreach (var collider in _colliders)
        {
            
        }
        
        
    }
    
    private void Fire()
    {
        
    }
}
