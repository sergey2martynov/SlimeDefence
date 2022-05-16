using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    [SerializeField] private  float _damage;
    [SerializeField]private  float _rate;
    [SerializeField]private  float _range;

    public virtual void UseWeapon()
    {
        
    }
}
