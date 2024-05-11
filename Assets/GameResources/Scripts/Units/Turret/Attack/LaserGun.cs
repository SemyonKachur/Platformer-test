namespace Units.Turret
{
    using System;
    using System.Collections;
    using Units.Turret.Bullets;
    using UnityEngine;

    public class LaserGun : AttackModule
    {
        private const float DELAY_TIME = 0.01f;
        private const float LASER_OFFSET = 0.5f;
        
        [SerializeField]
        private Transform _aimPoint = default;
        [SerializeField]
        private Laser _laser = default;
        [SerializeField]
        private float _attackTime = 3f;
        [SerializeField]
        private float _reloadTime = 2f;

        private bool _isReloading = default;
        private float _attacking = default;
        private float _reloading = default;
        
        private Ray _ray = default;
        private Coroutine _coroutine = default;
        
        public override void Attack(bool isAttack)
        {
            if (isAttack == IsAttack)
                return;
            
            IsAttack = isAttack;
            _laser.gameObject.SetActive(IsAttack);
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
                _ray = new Ray(_aimPoint.position, _laser.transform.forward);
                if (Physics.Raycast(_ray, out RaycastHit _info))
                {
                    if (_attacking < _attackTime && !_isReloading)
                    {
                        _laser.transform.localScale = new Vector3(1, 1, _info.distance + LASER_OFFSET);
                        _attacking += DELAY_TIME;
                        _reloading = 0;
                    }
                    else
                    {
                        _isReloading = true;
                        _laser.transform.localScale = new Vector3(1, 1, 0);
                        _attacking = 0;
                        _reloading += DELAY_TIME;
                    }

                    if (_reloading > _reloadTime)
                    {
                        _isReloading = false;
                    }
                }
                
                yield return new WaitForSeconds(DELAY_TIME);
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