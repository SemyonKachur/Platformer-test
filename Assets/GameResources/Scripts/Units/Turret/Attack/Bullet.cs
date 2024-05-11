namespace Units.Turret
{
    using System.Collections;
    using Units.Abstractions;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IDamageble
    {
        public float DamageValue => _damage;
        [SerializeField]
        private float _damage = 40;

        [SerializeField,Range(1,10)]
        private float _timeBeforeDestroy = 3;
        
        [SerializeField]
        private float _force = 10.0f;
        private Rigidbody _rigidbody = default;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void OnEnable()
        {
            _rigidbody.AddForce(transform.up * _force, ForceMode.Impulse);
            StartCoroutine(DelayedDestroy());
        }

        private IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(_timeBeforeDestroy);
            if (isActiveAndEnabled)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.collider.TryGetComponent<IHealthHolder>(out IHealthHolder healthHolder))
            {
                Damage(healthHolder);
            }
            Destroy(gameObject);
        }

        public void Damage(IHealthHolder health) => health.TakeDamage(this);
    }
}