namespace GameFlow.Conditions
{
    using Units.Player;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public class EndLevelZoneCondition : AbstractCondition
    {
        private Collider _collider = default;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            IsComplete = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                IsComplete = false;
            }
        }
    }
}
