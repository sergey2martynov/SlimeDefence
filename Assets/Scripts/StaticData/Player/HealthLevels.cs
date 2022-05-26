using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "HealthParameters", menuName = "StaticData/HealthParameters", order = 51)]
    public class HealthLevels : ScriptableObject
    {
        [SerializeField] private List<HealthParameters> _playerParameters;
    
        public HealthParameters GetHealthParameters(int level)
        {
            return _playerParameters[level];
        }
    }
}
