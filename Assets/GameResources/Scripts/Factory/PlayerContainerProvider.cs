namespace Factory
{
    using Units.Player;
    using UnityEngine;

    public class PlayerContainerProvider : MonoBehaviour
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

        protected virtual void DoAction() => player = _playerContainer.Player;

        protected virtual void OnDisable() => _playerContainer.onPlayerInit -= DoAction;
    }
}
