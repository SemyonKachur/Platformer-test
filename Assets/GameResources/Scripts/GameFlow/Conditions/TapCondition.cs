namespace GameFlow.Conditions
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class TapCondition : AbstractCondition
    {
        [SerializeField]
        private InputActionReference _playerInput = default;

        private void OnEnable()
        {
            _playerInput.action.Enable();
            _playerInput.action.performed += SetCondition;
        }

        private void SetCondition(InputAction.CallbackContext obj) => IsCompete = true;

        private void OnDisable() => _playerInput.action.performed -= SetCondition;
    }
}
