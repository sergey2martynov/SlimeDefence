using System;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrade
{
    [Serializable]
    public abstract class UpgradeParametersBase 
    {
        [SerializeField] private string _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _upgradeIcon;
        [SerializeField] private string _description;

        public string Name => _name;
        public Image Icon => _icon;
        public string Description => _description;

        public Image UprgradeIcon => _upgradeIcon;
    }
}
