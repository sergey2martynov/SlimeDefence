using System;
using CodeBase.Core.Character.Player;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Core.Character.Enemy
{
    public class EnemyMovementInput : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _target;

        public Transform Target => _target;

        private void Awake()
        {
            _target = FindObjectOfType<global::Core.Character.Player.Player>().transform;
            _agent.destination = _target.position;
        }

        public void MoveEnemy()
        {
            _agent.destination = _target.position;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out PlayerMovementInput player))
            {
                _agent.isStopped = true;
            }
        }
    }
}