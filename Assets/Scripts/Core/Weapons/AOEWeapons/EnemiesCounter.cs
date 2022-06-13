using CodeBase.Core.Character.Enemy;
using Core.Weapons;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private AOEWeapon _aoeWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null)
            {
                _aoeWeapon.EnemiesOnScreen.Add(enemy);
            }
        }
    }
}
