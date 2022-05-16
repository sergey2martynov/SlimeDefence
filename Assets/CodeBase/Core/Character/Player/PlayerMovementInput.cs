using System;
using UnityEngine;

namespace CodeBase.Core.Character.Player
{
    [RequireComponent(typeof(Movement))]
    public class PlayerMovementInput : MonoBehaviour
    {
        [SerializeField] private FixedJoystick _joystick;
        private Movement _movement;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
        }

        private void Update()
        {
            _movement.SetDirection(new Vector3(-_joystick.Horizontal, 0, -_joystick.Vertical));
        }
    }
}