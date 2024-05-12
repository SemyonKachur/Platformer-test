namespace GameFlow
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class AbstractGameState : MonoBehaviour
    {
        public event Action onStateValueChanged = delegate { };

        public string Id => id;
        [SerializeField]
        protected string id = default;
        
        public bool IsActive
        {
            get => isActive;
            protected set
            {
                if (isActive != value)
                {
                    isActive = value;
                    onStateValueChanged();
                }
            }
        }
        protected bool isActive = false;

        [SerializeField]
        protected List<AbstractCondition> conditions = new List<AbstractCondition>();

        protected virtual void OnEnable() => Init();

        protected virtual void OnDisable() => Dispose();

        protected virtual void Init()
        {
            foreach (AbstractCondition condition in conditions)
            {
                condition.onValueChanged += TryToComplete;
            }
        }

        protected virtual void Dispose()
        {
            foreach (AbstractCondition condition in conditions)
            {
                condition.onValueChanged -= TryToComplete;
            }
        }

        protected virtual void TryToComplete() => IsActive = IsConditionsComplete();

        protected virtual bool IsConditionsComplete()
        {
            foreach (AbstractCondition condition in conditions)
            {
                if (!condition.IsComplete)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
