using StaticData;
using StaticData.Player;
using UnityEngine;
using Upgrade;

public class Defence :Upgradable
{
    [SerializeField] private int _defence;
    [SerializeField] private DefenceLevels _defenceLevels;

    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public int DefencePlayer => _defence;

    private void Start()
    {
        _defence = _defenceLevels.GetDefenceParameters(_currentLevel).Amount;
    }

    public override void Upgrade()
    {
        _currentLevel++;
        _defence = _defenceLevels.GetDefenceParameters(_currentLevel).Amount;
    }

    public override UpgradeParametersBase GetUpgradeParameters()
    {
        return _defenceLevels.GetDefenceParameters(_currentLevel +1);
    }
}
