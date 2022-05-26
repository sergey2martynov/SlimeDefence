using UnityEngine;
using Upgrade;

public abstract class AbstractWeapon : MonoBehaviour, IUpgradable
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _rate;
    [SerializeField] protected float _range;

    protected UpgradeParametersBase _upgradeParameters;

    
    protected int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public int Damage => _damage;
    public float Rate => _rate;
    public float Range => _range;

    public virtual void UseWeapon()
    {
        
    }

    public virtual void Upgrade()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            return;
        }
        _currentLevel++;
    }

    public UpgradeParametersBase GetUpgradeParameters()
    {
        return _upgradeParameters;
    }
}
