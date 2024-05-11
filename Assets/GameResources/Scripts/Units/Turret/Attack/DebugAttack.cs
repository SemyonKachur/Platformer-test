namespace Units.Turret
{
    using UnityEngine;

    public class DebugAttack : AttackModule
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