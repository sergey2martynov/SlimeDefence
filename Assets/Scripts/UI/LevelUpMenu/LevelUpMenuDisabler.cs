using System.Net.NetworkInformation;
using UnityEngine;

public class LevelUpMenuDisabler : MonoBehaviour
{
    [SerializeField] private GameObject _levelUpMenu;

    public void LevelUpMenuDisable(bool isActive)
    {
        _levelUpMenu.SetActive(isActive);
    }
}
