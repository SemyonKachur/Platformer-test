namespace Units.Turret
{
    using UnityEngine;

    public sealed class MockAttack : AttackModule
    {
        public override void Attack(bool isAttack)
        {
            if (isAttack == IsAttack)
            {
                return;
            }

            IsAttack = isAttack;
            Debug.LogError($"attack mode {IsAttack}");
        }
    }
}