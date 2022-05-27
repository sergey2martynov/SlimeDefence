using System.Collections;
using Core.Character.Player;
using UnityEngine;

public class ObjectOfExpirience : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _experience;
    private ObjectOfExperiencePool _pool;

    public void Initialize(ObjectOfExperiencePool pool)
    {
        _pool = pool;
        StartCoroutine(DestroyOnTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            playerController.ProgressController.GetExperience(_experience);
            _pool.Pool.Release(gameObject);
        }
    }
    
    private IEnumerator DestroyOnTime()
    {
        yield return new WaitForSecondsRealtime(_lifeTime);
            _pool.Pool.Release(gameObject);
    }
}
