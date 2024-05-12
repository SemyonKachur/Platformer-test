namespace Units.Player.View
{
    using System;
    using Factory;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public class HealthTextView : PlayerContainerProvider
    {
        private Text _text = default;

        private void Awake() => _text = GetComponent<Text>();

        protected override void DoAction()
        {
            if (player != null)
            {
                player.onHealthChanged -= UpdateView;
            }

            player = _playerContainer.Player;
            UpdateView();
            player.onHealthChanged += UpdateView;
        }

        private void UpdateView()
        {
            if (player != null)
            {
                _text.text = player.Health.ToString();
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (player != null)
            {
                player.onHealthChanged -= UpdateView;
            }
        } 
    }
}
