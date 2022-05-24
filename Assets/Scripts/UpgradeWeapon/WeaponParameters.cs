using System;
using UnityEngine;

namespace UpgradeWeapon
{
    [Serializable]
    public class WeaponParameters
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _rate;
        [SerializeField] private float _range;

        public int Damage => _damage;
        public float Rate => _rate;
        public float Range => _range;
        
    }
}
