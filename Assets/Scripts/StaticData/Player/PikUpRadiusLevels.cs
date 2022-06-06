using System.Collections.Generic;
using UnityEngine;
using Upgrade.UpgradePlayer;

namespace StaticData.Player
{
    [CreateAssetMenu(fileName = "PikUpRadiusLevels", menuName = "StaticData/PikUpRadiusLevels", order = 51)]
    public class PikUpRadiusLevels : ScriptableObject
    {
        [SerializeField] private List<PikUpRadiusParameters> _playerParameters;

        public PikUpRadiusParameters GetRadiusParameters(int level)
        {
            return _playerParameters[level];
        }

        public int GetMaxNumberOfLevel()
        {
            return _playerParameters.Count - 1;
        }
    }
}