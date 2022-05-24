using System.Collections.Generic;
using UnityEngine;
using UpgradePlayer;

[CreateAssetMenu(fileName = "PlayerLevels", menuName = "PlayerLevels", order = 51)]
public class PlayerLevels : ScriptableObject
{
    [SerializeField] private List<PlayerParameters> _playerParameters;
    
    public PlayerParameters GetPlayerParameters(int level)
    {
        return _playerParameters[level - 1];
    }
}
