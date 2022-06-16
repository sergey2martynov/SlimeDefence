using System;
using UnityEngine;

namespace UpgradeWeapon
{
    [Serializable]
    public class ProjectileWeaponParameters : WeaponParameters
    {
        [SerializeField] private int _amount;
        [SerializeField] private float _spread;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private int _penetration;
        [SerializeField] private int _additionalProjectiles;
        
        public int Amount => _amount;
        public float Spread => _spread;
        public float ProjectileSpeed => _projectileSpeed;
        public int Penetration => _penetration;
        public int AdditionalProjectiles => _additionalProjectiles;
    }
}
