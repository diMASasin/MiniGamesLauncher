using System;
using UnityEngine;

namespace RPG
{
    public class CharacterMovement : MonoBehaviour, IDiractionChangable
    {
        [SerializeField] private float _speed;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _rotationSpeed = 20;

        private Vector3 _direction = new();
        private Vector3 _velocity = new();
        private IPlayerInput _input;

        public event Action<Vector3> DirectionChanged;

        public void Init(IPlayerInput input)
        {
            _input = input;
        }
        
        private void Update()
        {
            _direction.x = _input.GetHorizontalAxis();
            _direction.z = _input.GetVerticalAxis();

            _direction.Normalize();
            
            DirectionChanged?.Invoke(_direction);
            
            if (_direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            }

            _velocity = _direction * (_speed * Time.deltaTime);
            
            _controller.Move(_velocity);
        }
    }
}