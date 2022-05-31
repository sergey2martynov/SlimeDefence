using UnityEngine;

namespace Upgrade
{
    public abstract class Upgradable : MonoBehaviour
    {
        public int CurrentLevel { get; }

        private UpgradeParametersBase _upgradeParametersBase;

        public virtual void Upgrade()
        {
            
        }

        public virtual UpgradeParametersBase GetUpgradeParameters()
        {
            return _upgradeParametersBase;
        }
    }
}