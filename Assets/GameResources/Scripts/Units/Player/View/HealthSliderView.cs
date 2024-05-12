namespace Units.Player.View
{
    using Factory;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Slider))]
    public class HealthSliderView : PlayerContainerProvider
    {
        private Slider _slider = default;

        private void Awake() => _slider = GetComponent<Slider>();
        
        private void UpdateView()
        {
            if (player != null)
            {
                _slider.value = player.Health;
            }
        }

        protected override void DoAction()
        {
            if (player != null)
            {
                player.onHealthChanged -= UpdateView;
            }
            
            player = _playerContainer.Player;
            
            _slider.maxValue = player.MaxHealth;
            _slider.value = player.Health;
            _slider.wholeNumbers = true;
            _slider.interactable = false;
            
            UpdateView();
            player.onHealthChanged += UpdateView;
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
