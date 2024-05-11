namespace Units.Turret.States
{
    using System.Collections;
    using DG.Tweening;
    using Units.Player;
    using UnityEngine;

    [RequireComponent(typeof(SphereCollider))]
    public sealed class VisibleRangedAttackState : AbstractAiState
    {
        private SphereCollider _collider = default;

        [SerializeField,Range(1,20)]
        private float _attackRange = 10f;
        [SerializeField]
        private float _rangeMultiplier = 2;

        [SerializeField]
        private Vector3 _offest = new Vector3(0,0,2);
        [SerializeField, Range(0,10)]
        private float _speed = 4;

        private Transform _target = default;
        private Coroutine _coroutine = default;
        private Tweener _tweener = default;
        
        private Ray _ray = default;
        private RaycastHit _info = default;
        private Vector3 _position = default;
        private Quaternion _targetRotation = default;

        public override void Enter()
        {
            base.Enter();
            _coroutine = StartCoroutine(LookAtTarget());
        }

        private IEnumerator LookAtTarget()
        {
            while (isActive && _target != null)
            {
                _position = _target.position - turret.transform.position;
                _targetRotation = Quaternion.LookRotation(_position);
                turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, _targetRotation, Time.deltaTime * _speed);
                
                CheckPlayerVisible();
                yield return null;
            }
        }

        private void CheckPlayerVisible()
        {
            var direction = _target.position - turret.transform.position;
            _ray = new Ray(turret.transform.position + _offest, direction);
            if (Physics.Raycast(_ray, out RaycastHit _info, _attackRange * _rangeMultiplier))
            {
                if (_info.collider.TryGetComponent<Player>(out Player player))
                {
                    turret.Attack(true);
                }
                else
                {
                    turret.Attack(false);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            StopCoroutine(_coroutine);
            _coroutine = null;
        } 

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = _attackRange;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                _target = player.transform;
                turret.UpdateState(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                _target = null;
                OnStateCompleteHandler();
            }
        }
    }
}
