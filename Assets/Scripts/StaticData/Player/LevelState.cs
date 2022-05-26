using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelState", menuName = "StaticData", order = 51)]

public class LevelState : ScriptableObject
{
    [SerializeField] private List<int> _experience;

    public List<int> Experience => _experience;
}
