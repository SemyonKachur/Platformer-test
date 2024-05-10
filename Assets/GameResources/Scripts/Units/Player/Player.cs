namespace Units.Player
{
    using System;
    using Units.Abstractions;
    using UnityEngine;
    
    public class Player : MonoBehaviour, IHealthHolder
    {
        public event Action onHealthChanged = delegate { };

        public float Health
        {
            get => health;
            protected set
            {
                if (health != value)
                {
                    health = value;
                    onHealthChanged();
                }
            }
        }
        protected float health = default;

        public float MaxHealth => maxHealth;
        [SerializeField, Range(0,100)]
        protected float maxHealth = 100;

        protected void Awake() => Health = MaxHealth;
    }
}
