using CodeBase.Core.Character.Player;
using UnityEngine;

public class ObjectOfExpirience : MonoBehaviour
{
    [SerializeField] private int _expirience;
    private ObjectOfExperiencePool _pool;

    public void Initialize(ObjectOfExperiencePool pool)
    {
        _pool = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerControler playerController))
        {
            playerController.GetExperience(_expirience);
            _pool.Pool.Release(gameObject);
        }
    }
}
