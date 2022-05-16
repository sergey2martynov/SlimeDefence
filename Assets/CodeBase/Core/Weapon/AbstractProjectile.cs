using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class AbstractProjectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            gameObject.SetActive(false);
        }
    }
}
