using System.Collections.Generic;
using UnityEngine;
using UpgradeWeapon;

[CreateAssetMenu(fileName = "AttackShieldLevels", menuName = "StaticData/AttackShieldLevels", order = 51)]
public class AttackShieldLevels : ScriptableObject
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
 