using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StagesLevel", menuName = "StaticData/StagesLevel", order = 51)]
public class StagesLevel : ScriptableObject
{
    [SerializeField] private List<int> _timesOfSpawnBosses;

    [SerializeField] private int _levelDuration;

    public List<int> TimesOfSpawnBosses => _timesOfSpawnBosses;

    public int LevelDuration => _levelDuration;
}
