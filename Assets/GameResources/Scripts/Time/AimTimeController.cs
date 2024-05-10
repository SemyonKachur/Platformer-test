namespace Time
{
    using GameFlow;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public sealed class AimTimeController : MonoBehaviour
    {
        [SerializeField]
        private AbstractGameState _activeState = default;
        [SerializeField]
        private InputActionReference _aimInput = default;

        [SerializeField, Range(0, 1)]
        private float _aimTimeScale = 0.1f;

        private float _defaultTimeScale = 1;

        private void OnEnable()
        {
            _aimInput.action.performed += TryToSetTimeScale;
            _aimInput.action.canceled += TrySetTimeScaleToDefault;
        }

        private void TrySetTimeScaleToDefault(InputAction.CallbackContext context)
        {
            if (_activeState.IsActive)
            {
                Time.timeScale = _defaultTimeScale;
            }
        }

        private void TryToSetTimeScale(InputAction.CallbackContext context)
        {
            if (_activeState.IsActive)
            {
                Time.timeScale = _aimTimeScale;
            }
        }

        private void OnDisable()
        {
            _aimInput.action.performed -= TryToSetTimeScale;
            _aimInput.action.canceled -= TrySetTimeScaleToDefault;
        }
    }
}
