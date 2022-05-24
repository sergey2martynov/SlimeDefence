using System;
using UnityEngine;

namespace UpgradePlayer
{
    [Serializable]
    public class PlayerParameters
    {
        [SerializeField] private int _health;
        [SerializeField] private float _speed;
        [SerializeField] private float _pikUpRadius;

        public int Health => _health;
        public float Speed => _speed;
        public float PikUpRadius => _pikUpRadius;
    }
}