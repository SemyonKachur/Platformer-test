namespace Units.Turret
{
    using System.Collections;
    using UnityEngine;

    public sealed class BulletGun : AttackModule
    {
        [SerializeField]
        private Transform _spawnPoint = default;
        [SerializeField]
        private float _delayTime = 0.5f;
        [SerializeField]
        private Bullet _bullet = default;

        private bool _isAvailableToShoot => (Time.time - _lastShootTime) > _delayTime;
        private float _lastShootTime = default;
        private Coroutine _coroutine = default;
        
        public override void Attack(bool isAttack)
        {
            if (isAttack == IsAttack)
                return;
            
            IsAttack = isAttack;
            if (IsAttack)
            {
                ResetCoroutine();
                _coroutine = StartCoroutine(Shoot());
            }
        }

        private IEnumerator Shoot()
        {
            while (isActiveAndEnabled && IsAttack)
            {
                if (_isAvailableToShoot)
                {
                    Instantiate(_bullet, _spawnPoint.position, _spawnPoint.transform.rotation);
                    _lastShootTime = Time.time;
                }
                yield return new WaitForSeconds(_delayTime);
            }
        }

        private void ResetCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }
}