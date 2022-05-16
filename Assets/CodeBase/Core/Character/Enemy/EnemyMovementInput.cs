using UnityEngine;
using CodeBase.Core.Character.Player;

namespace CodeBase.Core.Character.Enemy
{
    [RequireComponent(typeof(Movement))]
    public class EnemyMovementInput : MonoBehaviour
    {
        private Movement _movement;
        private Transform _target;
        
        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _target = FindObjectOfType<PlayerControler>().transform;
        }

        private void Update()
        {
            var direction = _target.position - transform.position;
            _movement.SetDirection(direction);
        }
    }
}