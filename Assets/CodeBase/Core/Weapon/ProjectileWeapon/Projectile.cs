using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; set; }
    
    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
        
        if (enemyController != null)
        {
            enemyController.Health.GetDamage(Damage);
            gameObject.SetActive(false);
        }
    }
}
