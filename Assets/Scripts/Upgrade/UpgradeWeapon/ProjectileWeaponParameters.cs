using System;
using UnityEngine;

namespace UpgradeWeapon
{
    [Serializable]
    public class ProjectileWeaponParameters : WeaponParameters
    {
        [SerializeField] private int _amount;
        [SerializeField] private float _spread;
        
        public int Amount => _amount;
        public float Spread => _spread;
        
    }
}
