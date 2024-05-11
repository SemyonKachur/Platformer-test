namespace Units.Turret
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TurretBase : MonoBehaviour
    {
        public event Action onHealthChanged = delegate { };

        public event Action onDeath = delegate { };
        
        public float Health
        {
            get => health;
            protected set
            {
                if (health != value)
                {
                    health = value;
                    onHealthChanged();

                    if (health <= 0)
                    {
                        onDeath();
                    }
                }
            }
        }
        protected float health = default;

        public float MaxHealth => maxHealth;
        [SerializeField, Range(0,100)]
        protected float maxHealth = 50;

        [SerializeField]
        protected AbstractAiState activeState = default;
        
        [SerializeField]
        protected List<AbstractAiState> aiStates = new List<AbstractAiState>();
        [SerializeField, Min(0)]
        protected int defaultFirstState = 0;

        [SerializeField]
        protected AttackModule attackModule = default;
        
        public virtual void UpdateState(AbstractAiState state)
        {
            if (activeState != null)
            {
                activeState.Exit();
            }

            activeState = state;
            activeState.Enter();
        }

        public virtual void Attack(bool isAttack) => attackModule.Attack(isAttack);

        protected virtual void Awake()
        {
            InitStats();
            InitAI();
        }

        protected virtual void InitStats() => Health = MaxHealth;

        protected virtual void InitAI()
        {
            foreach (AbstractAiState aiState in aiStates)
            {
                aiState.Init(this);
            }
            
            activeState = aiStates[defaultFirstState];
            activeState.Enter();
        }

        protected virtual void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                if (defaultFirstState >= aiStates.Count && defaultFirstState != 0)
                {
                    defaultFirstState = aiStates.Count - 1;
                }
            }
        }

        protected virtual void OnDisable()
        {
            activeState.Exit();
            Attack(false);
        } 
    }
}