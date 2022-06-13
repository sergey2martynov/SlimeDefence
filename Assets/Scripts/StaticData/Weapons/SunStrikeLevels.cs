using System.Collections.Generic;
using UnityEngine;
using UpgradeWeapon;

[CreateAssetMenu(fileName = "SunStrikeLevels", menuName = "StaticData/SunStrikeLevels", order = 51)]
public class SunStrikeLevels : ScriptableObject
{
    [SerializeField] private List<WeaponParameters> _weaponParameters;

    public WeaponParameters GetWeaponParameters(int level)
    {
        return _weaponParameters[level ];
    }
    
    public int GetMaxNumberOfLevel()
    {
        return _weaponParameters.Count - 1;
    }
}
 