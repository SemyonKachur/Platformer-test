namespace Units.Turret.States
{
    using System.Collections;
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
        [SerializeField, Range(0,1)]
        private float _delayTime = 0.05f;

        private Transform _target = default;
        private Coroutine _coroutine = default;
        
        private Ray _ray = default;
        private RaycastHit _info = default;

        public override void Enter()
        {
            base.Enter();
            _coroutine = StartCoroutine(LookAtTarget());
        }

        private IEnumerator LookAtTarget()
        {
            while (isActive)
            {
                turret.transform.LookAt(_target);
                CheckPlayerVisible();
                yield return new WaitForSeconds(_delayTime);
            }
        }

        private void CheckPlayerVisible()
        {
            _ray = new Ray(turret.transform.position + _offest, turret.transform.forward);
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
                turret.UpdateState(this);
                _target = null;
                OnStateCompleteHandler();
            }
        }
    }
}
