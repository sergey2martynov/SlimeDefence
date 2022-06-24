using UnityEngine;

namespace Upgrade
{
    public abstract class Upgradable : MonoBehaviour
    {
        
        [SerializeField] public bool IsActive;
        
        protected int _currentLevel;
        public int MaxLevel { get; set; }
        public int CurrentLevel => _currentLevel;

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