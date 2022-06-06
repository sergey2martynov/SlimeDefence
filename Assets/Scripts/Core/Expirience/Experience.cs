using System.Collections;
using Core.Character.Player;
using DG.Tweening;
using UnityEngine;

namespace Core.Expirience
{
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
            if (other.gameObject.TryGetComponent(out PikUpRadius radius))
            {
                radius.Controller.ProgressController.GetExperience(_experience);
                //radius.ColliderDisable();
                StartCoroutine(MoveToPlayer(radius.transform));
            }
            
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _pool.Pool.Release(gameObject);
            }
        }
        private IEnumerator MoveToPlayer(Transform player)
        {
            yield return transform.DOMove(player.position, 0.5f).OnComplete(() => _pool.Pool.Release(gameObject));
        }
        private IEnumerator DestroyOnTime()
        {
            yield return new WaitForSecondsRealtime(_lifeTime);
            _pool.Pool.Release(gameObject);
        }
    }
}
