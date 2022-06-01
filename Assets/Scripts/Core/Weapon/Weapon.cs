using UnityEngine;
using Upgrade;

public abstract class Weapon : Upgradable
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _rate;
    [SerializeField] protected float _range;

    protected UpgradeParametersBase _upgradeParameters;

    public int Damage => _damage;
    public float Rate => _rate;
    public float Range => _range;

    public virtual void UseWeapon()
    {
        
    }

    public override void Upgrade()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            return;
        }
        _currentLevel++;
    }

    public override UpgradeParametersBase GetUpgradeParameters()
    {
        return _upgradeParameters;
    }
}
