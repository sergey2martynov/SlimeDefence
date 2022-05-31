using System.Collections;
using Core.Character.Player;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _experience;
    private ExperiencePool _pool;

    public void Initialize(ExperiencePool pool)
    {
        _pool = pool;
        StartCoroutine(DestroyOnTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player playerController))
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
