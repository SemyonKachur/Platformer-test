namespace GameFlow
{
    using System;
    using UnityEngine;

    public abstract class AbstractCondition : MonoBehaviour
    {
        public event Action onValueChanged = delegate { };

        public bool IsCompete
        {
            get => isComplete;
            protected set
            {
                if (isComplete != value)
                {
                    isComplete = value;
                    onValueChanged();
                }
            }
        }
        protected bool isComplete = default;
    }
}