using System;
using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using Core.Weapons;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    public List<Enemy> EnemiesOnScreen { get;  set; }

    private void Start()
    {
        EnemiesOnScreen = new List<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null)
            {
                EnemiesOnScreen.Add(enemy);
            }
        }
    }
}
