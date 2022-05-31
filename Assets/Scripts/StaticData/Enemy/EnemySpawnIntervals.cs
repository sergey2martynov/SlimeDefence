using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemySpawnIntervals", menuName = "StaticData/EnemySpawnIntervals", order = 51)]
    public class EnemySpawnIntervals : ScriptableObject
    {
        [SerializeField] private List<float> _firstCurveValue;
        [SerializeField] private List<float> _lastCurveValue;

        public List<float> FirstCurveValue => _firstCurveValue;
        public List<float> LastCurveValue => _lastCurveValue;
    }
}