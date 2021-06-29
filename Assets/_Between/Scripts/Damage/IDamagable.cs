using Between.Damage;
using Between.Teams;

namespace Between.Interfaces
{
    public interface IDamagable
    {
        Team Team { get; }
        void ApplyDamage(DamageType type, float damage);
    }
}