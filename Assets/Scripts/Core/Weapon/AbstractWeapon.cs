using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _rate;
    [SerializeField] protected float _range;

    
    protected int _currentLevel = 1;

    public int Damage => _damage;
    public float Rate => _rate;
    public float Range => _range;

    public virtual void UseWeapon()
    {
        
    }

    public virtual void Upgrade()
    {
        _currentLevel++;
    }
}
