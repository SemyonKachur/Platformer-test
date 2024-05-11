namespace Units.Turret
{
    using System;
    using UnityEngine;

    public abstract class AttackModule : MonoBehaviour
    {
        public event Action onAttackStateChanged = delegate { };

        public bool IsAttack
        {
            get => isAttack;
            protected set
            {
                if (isAttack != value)
                {
                    isAttack = value;
                    onAttackStateChanged();
                }
            }
        }
        protected bool isAttack = false;

        public abstract void Attack(bool isAttack);
    }
}