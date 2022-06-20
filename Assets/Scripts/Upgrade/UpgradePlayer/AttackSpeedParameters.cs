using System;
using UnityEngine;

namespace Upgrade.UpgradePlayer
{
    [Serializable]
    public class AttackSpeedParameters : UpgradeParametersBase
    {
        [SerializeField] private float _amount;

        public float Amount => _amount;
    }
}