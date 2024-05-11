namespace Units.Abstractions
{
    using Units.Turret;

    public interface IHealthHolder
    {
        float Health { get; }
        float MaxHealth { get; }

        void TakeDamage(IDamageble damage);
    }
}