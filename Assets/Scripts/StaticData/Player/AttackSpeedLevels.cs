using System.Collections.Generic;
using UnityEngine;
using Upgrade.UpgradePlayer;

namespace StaticData.Player
{
    [CreateAssetMenu(fileName = "AttackSpeedParameters", menuName = "StaticData/AttackSpeedParameters", order = 51)]
    public class AttackSpeedLevels : ScriptableObject
    {
        [SerializeField] private List<AttackSpeedParameters> _playerParameters;
    
        public AttackSpeedParameters GetAttackSpeedParameters(int level)
        {
            return _playerParameters[level];
        }

        public int GetMaxNumberOfLevel()
        {
            return _playerParameters.Count - 1;
        }
    }
}
