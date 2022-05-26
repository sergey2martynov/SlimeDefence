using System.Collections.Generic;
using UnityEngine;
using UpgradeWeapon;

[CreateAssetMenu(fileName = "WeaponLevels", menuName = "WeaponLevels", order = 51)]
public class ProjectileWeaponLevels : ScriptableObject
{
    [SerializeField] private List<ProjectileWeaponParameters> _weaponParameters;

    public ProjectileWeaponParameters GetWeaponParameters(int level)
    {
        return _weaponParameters[level ];
    }
}
 