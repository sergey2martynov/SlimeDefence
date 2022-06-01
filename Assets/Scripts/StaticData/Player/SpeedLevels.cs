using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "SpeedParameters", menuName = "StaticData/SpeedParameters", order = 51)]
    public class SpeedLevels : ScriptableObject
    {
        [SerializeField] private List<SpeedParameters> _speedParameters;
    
        public SpeedParameters GetSpeedParameters(int level)
        {
            return _speedParameters[level];
        }
        
        public int GetMaxNumberOfLevel()
        {
            return _speedParameters.Count - 1;
        }
    }
}
