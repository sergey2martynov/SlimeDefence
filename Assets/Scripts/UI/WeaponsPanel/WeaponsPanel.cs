using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WeaponsPanel
{
    public class WeaponsPanel : MonoBehaviour
    {
        [SerializeField] private List<Image> _weaponIcons;
        [SerializeField] private Weapon _defaultWeapon;

        private List<Weapon> _activeWeapon;

        private void Start()
        {
            _activeWeapon = new List<Weapon>();
            _activeWeapon.Add(_defaultWeapon);
            _weaponIcons[0].sprite = _defaultWeapon.GetUpgradeParameters().Icon.sprite;
        }

        public void UpdatePanel(Weapon weapon, bool isNewWeapon)
        {
            if (isNewWeapon)
            {
                _activeWeapon.Add(weapon);
            }
            
            for (int i = 0; i < _activeWeapon.Count; i++)
            {
                if (weapon == _activeWeapon[i])
                {
                    _weaponIcons[i].sprite = _activeWeapon[i].GetUpgradeParameters().Icon.sprite;
                }
            }
        }
    }
}
