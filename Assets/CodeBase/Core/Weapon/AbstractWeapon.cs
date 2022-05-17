using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    [SerializeField] private  int _damage;
    [SerializeField] private  float _rate;
    [SerializeField] private  float _range;

    public int Damage => _damage;
    public float Rate => _rate;
    public float Range => _range;

    public virtual void UseWeapon()
    {
        
    }
}
