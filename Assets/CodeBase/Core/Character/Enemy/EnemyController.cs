using System;
using UnityEditor.Build;
using UnityEngine;

namespace CodeBase.Core.Character.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Health _health;

        public Health Health => _health;
        public bool IsDie { get; private set; }

        private void Start()
        {
            _health.HealthIsOver += Die;
        }

        private void OnDestroy()
        {
            _health.HealthIsOver -= Die;
        }

        private void Die()
        {
            IsDie = true;
            gameObject.SetActive(false);
        }
    }
}