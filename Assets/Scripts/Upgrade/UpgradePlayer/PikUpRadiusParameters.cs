using System;
using UnityEngine;

namespace Upgrade.UpgradePlayer
{
    [Serializable]
    public class PikUpRadiusParameters : UpgradeParametersBase
    {
        [SerializeField] private int _amount;

        public int Amount => _amount;
    }
}