using System;
using UnityEngine;
using Upgrade;

[Serializable]
public class DefenceParameters : UpgradeParametersBase
{
    [SerializeField] private int _amount;

    public int Amount => _amount;
}