namespace Units.Turret.Bullets
{
    using Units.Abstractions;
    using UnityEngine;
    public class Laser : MonoBehaviour, IDamageble
    {
        public float DamageValue => _damage;
        [SerializeField]
        private float _damage = 100;
        
        private void OnCollisionStay(Collision other)
        {
            if(other.collider.TryGetComponent<IHealthHolder>(out IHealthHolder healthHolder))
            {
                Debug.LogError("Damage");
                Damage(healthHolder);
            }
        }
        
        public void Damage(IHealthHolder health) => health.TakeDamage(this);
    }
}
