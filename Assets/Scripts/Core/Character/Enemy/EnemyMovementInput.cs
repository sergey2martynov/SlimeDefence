using UnityEngine;
using CodeBase.Core.Character.Player;

namespace CodeBase.Core.Character.Enemy
{
    [RequireComponent(typeof(Movement))]
    public class EnemyMovementInput : MonoBehaviour
    {
        [SerializeField] private float _minDistance;

        private Movement _movement;
        private Transform _target;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _target = FindObjectOfType<PlayerController>().transform;
        }

        private void Update()
        {
            var distance = (_target.position - transform.position).magnitude;

            if (distance > _minDistance)
            {
                var direction = _target.position - transform.position;
                _movement.SetDirection(direction);
            }
        }
    }
}