using System.Collections.Generic;
using UnityEngine;
using UpgradeWeapon;

[CreateAssetMenu(fileName = "RailGunLevels", menuName = "StaticData/RailGunLevels", order = 51)]
public class RailGunLevels : ScriptableObject
{
    [SerializeField] private List<RailGunParameters> _weaponParameters;

    public RailGunParameters GetWeaponParameters(int level)
    {
        return _weaponParameters[level ];
    }
    
    public int GetMaxNumberOfLevel()
    {
        return _weaponParameters.Count - 1;
    }
}
 