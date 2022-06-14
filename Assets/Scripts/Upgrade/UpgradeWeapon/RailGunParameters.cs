using System;
using UnityEngine;

namespace UpgradeWeapon
{
    [Serializable]
    public class RailGunParameters : WeaponParameters
    {
        [SerializeField] private float _projectileSpeed;
        
        public float ProjectileSpeed => _projectileSpeed;
    }
}
