using System.Collections;
using Character.Player;
using Core.Character.Player;
using DG.Tweening;
using UnityEngine;

namespace Core.Expirience
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private int _experience;
        [SerializeField] private bool _isBig;
        [SerializeField] private float _speed = 0.2f;
        private Transform _player;
        private ExperiencePool _pool;
        private bool _isCanMoveToPlayer;
        private PikUpRadius _radius;
        

        public void Initialize(ExperiencePool pool, Transform player)
        {
            _pool = pool;
            StartCoroutine(DestroyOnTime());
            _player = player;
        }

        private void FixedUpdate()
        {
            if(_isCanMoveToPlayer)
                transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PikUpRadius radius))
            {
                _radius = radius;
                
                if (!_isCanMoveToPlayer)
                {
                    StartCoroutine(MoveToPlayer(radius.transform));
                }
            }
            
            if (other.gameObject.TryGetComponent(out Player player))
            {
                _radius.Controller.ProgressController.GetExperience(_experience);
                
                if (_isBig)
                    Destroy(gameObject);
                else
                    _pool.Release(gameObject);
                
                _isCanMoveToPlayer = false;
            }
        }
        private IEnumerator MoveToPlayer(Transform player)
        {
            var offset = new Vector3(0, 1, 0);
            yield return transform.DOMove(transform.position + offset, 0.5f).OnComplete(() => _isCanMoveToPlayer = true);
        }
        private IEnumerator DestroyOnTime()
        {
            yield return new WaitForSecondsRealtime(_lifeTime);
            _pool.Release(gameObject);
        }
    }
}
