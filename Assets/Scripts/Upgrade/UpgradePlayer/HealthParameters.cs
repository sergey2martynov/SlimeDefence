using System;
using UnityEngine;
using Upgrade;

[Serializable]
public class HealthParameters : UpgradeParametersBase
{
    [SerializeField] private int _amount;

    public int Amount => _amount;
}
