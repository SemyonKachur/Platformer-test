namespace Units.Player.View
{
    using System.Collections;
    using Factory;
    using Units.Abstractions;
    using UnityEngine;

    public class SwipeView : PlayerContainerProvider
    {
        [SerializeField,Range(0,1000)]
        private int _directionDevider = 500;
        [SerializeField,Range(-10,10)]
        private int _zPositionOffset = 5;
        
        private IMovable _movable = default;
        private Coroutine _coroutine = default;

        private bool _isTargeting = default;

        protected override void DoAction()
        {
            player = _playerContainer.Player;
            _movable = player.GetComponent<IMovable>();
            if (_movable != null)
            {
                _movable.onStartMove += ShowView;
                _movable.onEndMove += SkipView;
            }
        }

        private void SkipView()
        {
            _isTargeting = false;
            transform.localScale = Vector3.zero;
        }

        private void ShowView()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(SetDirection());
        }

        private IEnumerator SetDirection()
        {
            _isTargeting = true;
            while (isActiveAndEnabled && _isTargeting)
            {
                transform.position = new Vector3(
                    player.transform.position.x + _movable.Direction.x/_directionDevider, 
                    player.transform.position.y + _movable.Direction.y/_directionDevider, 
                    player.transform.position.z + _zPositionOffset);
                
                transform.rotation = Quaternion.LookRotation(transform.forward, _movable.Direction);
                transform.localScale = new Vector3(1, _movable.Direction.magnitude/_directionDevider, 1);
                yield return null;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (_movable != null)
            {
                _movable.onStartMove -= ShowView;
                _movable.onEndMove -= SkipView;
            }
        } 
    }
}
