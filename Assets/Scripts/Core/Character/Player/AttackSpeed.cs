using StaticData.Player;
using UnityEngine;
using Upgrade;

namespace Core.Character.Player
{
    public class AttackSpeed :Upgradable
    {
        [SerializeField] private float _attackSpeed;
        [SerializeField] private AttackSpeedLevels _attackSpeedLevels;

        public float AttackSpeedValue => _attackSpeed;
    
        private void Start()
        {
            MaxLevel = _attackSpeedLevels.GetMaxNumberOfLevel();
            _attackSpeed = _attackSpeedLevels.GetAttackSpeedParameters(_currentLevel).Amount;
        }

        public override void Upgrade()
        {
            _currentLevel++;
            _attackSpeed = _attackSpeedLevels.GetAttackSpeedParameters(_currentLevel).Amount;
        }

        public override UpgradeParametersBase GetUpgradeParameters()
        {
            return _attackSpeedLevels.GetAttackSpeedParameters(_currentLevel +1);
        }

    }
}
