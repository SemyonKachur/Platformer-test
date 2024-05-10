namespace Units.Abstractions
{
    using System;
    using UnityEngine;

    public interface IMovable
    {
        public event Action onStartMove;
        public event Action onEndMove;

        public Vector2 Direction { get; }
        
        public void Move();
    }
}