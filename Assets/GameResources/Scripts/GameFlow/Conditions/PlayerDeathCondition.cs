namespace GameFlow.Conditions
{
    using System;
    using Factory;
    using Units.Player;
    using UnityEngine;

    public class PlayerDeathCondition : AbstractCondition
    {
        [SerializeField]
        private PlayerContainerInstance _playerContainer = default;

        private Player _player = default;

        private void OnEnable()
        {
            IsComplete = true;
            if (_playerContainer.Player != null)
            {
                Subscribe();
            }
            else
            {
                _playerContainer.onPlayerInit += Subscribe;
            }
        }

        private void Subscribe()
        {
            if (_player != null)
            {
                _player.onHealthChanged -= UpdateConditionValue;
            }

            _player = _playerContainer.Player;
            _player.onHealthChanged += UpdateConditionValue;
        }

        private void UpdateConditionValue() => IsComplete = _player.Health > 0;

        private void OnDisable()
        {
            _playerContainer.onPlayerInit -= Subscribe;
            
            if (_player != null)
            {
                _player.onHealthChanged -= UpdateConditionValue;
            }
        }
    }
}
