using UnityEngine;
using CodeBase.Core.Character.Player;
using Core.Character.Player;
using UnityEngine.Apple.ReplayKit;

namespace CodeBase.Core.Character.Enemy
{
    [RequireComponent(typeof(Movement))]
    public class EnemyMovementInput : MonoBehaviour
    {
        [SerializeField] private float _minDistance;
        [SerializeField] private Enemy _enemy;

        private Movement _movement;
        private Transform _target;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _target = FindObjectOfType<global::Core.Character.Player.Player>().transform;
        }

        private void Update()
        {
            var distance = (_target.position - transform.position).magnitude;

            if (distance > _minDistance )
            {
                var direction = _target.position - transform.position;
                _movement.SetDirection(direction);
                _movement.SetLookDirection(direction);
            }
        }

        // private bool IsEnemyOnTheWay()
        // {
        //     RaycastHit hit;
        //     
        //     Ray ray = new Ray(transform.position, (_target.position - transform.position));
        //     
        //     Physics.Raycast(ray, out hit);
        //     
        //     Debug.DrawLine(ray.origin, hit.point,Color.red);
        //     
        //     if (hit.collider.gameObject.TryGetComponent(out Enemy enemy))
        //     {
        //         for (int i = 0; i < _enemy.EnemiesAround.Count; i++)
        //         {
        //             if (enemy == _enemy.EnemiesAround[i])
        //                 return true;
        //         }
        //     }
        //     
        //     return false;
        // }
    }
}