namespace Units.Abstractions
{
    public interface IDamageble
    {
        float DamageValue { get; }
        void Damage(IHealthHolder health);
    }
}