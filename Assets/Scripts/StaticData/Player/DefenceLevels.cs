using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Player
{
    [CreateAssetMenu(fileName = "DefenceParameters", menuName = "StaticData/DefenceParameters", order = 51)]
    public class DefenceLevels : ScriptableObject
    {
        [SerializeField] private List<DefenceParameters> _playerParameters;
    
        public DefenceParameters GetDefenceParameters(int level)
        {
            return _playerParameters[level];
        }
    }
}
