namespace GameFlow.Conditions
{
    using System.Collections.Generic;
    using UnityEngine;

    public class EndGameCondition : AbstractCondition
    {
        [SerializeField]
        protected List<AbstractCondition> _endGameConditions = new List<AbstractCondition>();

        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe()
        {
            foreach (AbstractCondition condition in _endGameConditions)
            {
                condition.onValueChanged += TryToComplete;
            }
        }

        private void Unsubscribe()
        {
            foreach (AbstractCondition condition in _endGameConditions)
            {
                condition.onValueChanged -= TryToComplete;
            }
        }

        protected virtual void TryToComplete() => IsComplete = IsConditionsComplete();
        
        protected virtual bool IsConditionsComplete()
        {
            foreach (AbstractCondition condition in _endGameConditions)
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
