using System.Collections;
using CodeBase.Core.Character.Player;
using Core.Character.Player;
using UnityEngine;

public class ObjectOfExpirience : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _expirience;
    private ObjectOfExperiencePool _pool;

    public void Initialize(ObjectOfExperiencePool pool)
    {
        _pool = pool;
        StartCoroutine(DestoryOnTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            playerController.ProgressController.GetExperience(_expirience);
            _pool.Pool.Release(gameObject);
        }
    }
    
    private IEnumerator DestoryOnTime()
    {
        yield return new WaitForSecondsRealtime(_lifeTime);
            _pool.Pool.Release(gameObject);
    }
}
