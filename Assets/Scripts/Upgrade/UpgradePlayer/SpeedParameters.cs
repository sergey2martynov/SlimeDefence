using System;
using UnityEngine;
using Upgrade;

[Serializable]
public class SpeedParameters : UpgradeParametersBase
{
    [SerializeField] private int _amount;

    public int Amount => _amount;
}