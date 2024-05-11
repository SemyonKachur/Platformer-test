namespace Units.Turret
{
    using System;
    using UnityEngine;

    public abstract class AbstractAiState : MonoBehaviour
    {
        public event Action onStateComplete = delegate { };

        public string Id => id;
        [SerializeField]
        protected string id = default;

        protected bool isInit = default;

        protected TurretBase turret = default;
        protected bool isActive = default;

        public virtual void Init(TurretBase turretBase)
        {
            if (isInit)
                return;

            isInit = true;
            turret = turretBase;
        }
        
        public virtual void Enter() => isActive = true;

        public virtual void Exit() => isActive = false;

        protected virtual void OnStateCompleteHandler() => onStateComplete();
    }
}