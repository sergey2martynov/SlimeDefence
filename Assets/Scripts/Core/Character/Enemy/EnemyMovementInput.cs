using System;
using CodeBase.Core.Character.Player;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Core.Character.Enemy
{
    public class EnemyMovementInput : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;
        
        private Transform _target;

        private void Awake()
        {
            _target = FindObjectOfType<global::Character.Player.Player>().transform;
            _agent.destination = _target.position;
            _agent.speed = _speed;
        }

        public void MoveEnemy()
        {
            _agent.destination = _target.position;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out PlayerMovementInput player))
            {
                _agent.speed = 0;
            }
        }

        public void ReturnSpeed()
        {
            _agent.speed = _speed;
        }
    }
}