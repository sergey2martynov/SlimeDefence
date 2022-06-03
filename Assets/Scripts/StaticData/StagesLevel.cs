using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StagesLevel", menuName = "StaticData/StagesLevel", order = 51)]
public class StagesLevel : ScriptableObject
{
    [SerializeField] private List<WaveParameters> _waveParameters;
    public List<WaveParameters> WaveParameters => _waveParameters;

    public WaveParameters GetWaveParameters(int index)
    {
        return _waveParameters[index];
    }
}
