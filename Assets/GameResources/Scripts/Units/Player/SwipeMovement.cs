﻿namespace Units.Player
{
    using System;
    using Abstractions;
    using UnityEngine;
    using UnityEngine.InputSystem;

    [RequireComponent(typeof(Rigidbody))]
    public class SwipeMovement : MonoBehaviour, IMovable
    {
        public event Action onStartMove = delegate { };
        public event Action onEndMove = delegate { };

        public Vector2 Direction => _direction;
        private Vector2 _direction = default;

        [SerializeField]
        private InputActionReference _aim = default;
        [SerializeField]
        private InputActionReference _position = default;
        
        [SerializeField]
        private ForceMode _forceMode = ForceMode.Impulse;
        [SerializeField,Range(10,1000)]
        private float _swipeTrashHold = 100;
        [SerializeField, Range(100, 2000)]
        private float _maxMovePower = 1000;
        
        private Vector2 _delta = default;
        private Vector2 _startPosition = default;
        private Vector2 _currentPosition => _position.action.ReadValue<Vector2>();

        private Rigidbody _rigidbody = default;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnEnable()
        {
            _position.action.Enable();
            _aim.action.Enable();
            _aim.action.performed += StartMove;
            _aim.action.canceled += DetectSwipe;
        }

        private void StartMove(InputAction.CallbackContext context)
        {
            _startPosition = _currentPosition;
            _position.action.performed += UpdateDirection;
            onStartMove();
        }

        private void UpdateDirection(InputAction.CallbackContext context) => CalculateDirection();

        private void DetectSwipe(InputAction.CallbackContext context)
        {
            _position.action.performed -= UpdateDirection;
            CalculateDirection();
            Move();
            onEndMove();
        }

        private void CalculateDirection()
        {
            _delta = _currentPosition - _startPosition;
            _direction = Vector2.zero;

            if (Mathf.Abs(_delta.x) > _swipeTrashHold)
                _direction.x = Mathf.Clamp(_delta.x, -_maxMovePower, _maxMovePower);

            if (Mathf.Abs(_delta.y) > _swipeTrashHold)
                _direction.y = Mathf.Clamp(_delta.y, -_maxMovePower, _maxMovePower);
        }

        public void Move()
        {
            if (_direction != Vector2.zero)
            {
                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.AddForce(Direction, _forceMode);
            }
        } 

        private void OnDisable()
        {
            _aim.action.Disable();
            _aim.action.performed -= UpdateDirection;
            
            _position.action.Disable();
            _aim.action.performed -= StartMove;
            _aim.action.canceled -= DetectSwipe;
        }
    }
}