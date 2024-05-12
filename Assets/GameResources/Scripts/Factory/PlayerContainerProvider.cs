namespace Factory
{
    using Units.Player;
    using UnityEngine;

    public abstract class PlayerContainerProvider : MonoBehaviour
    {
        [SerializeField]
        protected PlayerContainerInstance _playerContainer = default;

        protected Player player = default;

        protected virtual void OnEnable()
        {
            if (_playerContainer.Player != null)
            {
                DoAction();
            }
            else
            {
                _playerContainer.onPlayerInit += DoAction;
            }
        }

        protected abstract void DoAction(); 

        protected virtual void OnDisable() => _playerContainer.onPlayerInit -= DoAction;
    }
}
