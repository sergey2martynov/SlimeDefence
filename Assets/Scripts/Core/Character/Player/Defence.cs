using Core.Character;
using StaticData.Player;
using UnityEngine;
using Upgrade;

public class Defence :Upgradable
{
    [SerializeField] private int _defence;
    [SerializeField] private DefenceLevels _defenceLevels;
    [SerializeField] private CharacterType _characterType;

    public int DefencePlayer => _defence;

    private void Start()
    {
        if (_characterType == CharacterType.Player)
        {
            MaxLevel = _defenceLevels.GetMaxNumberOfLevel();
            _defence = _defenceLevels.GetDefenceParameters(_currentLevel).Amount;
        }
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
