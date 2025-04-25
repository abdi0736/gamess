using Gamess.Strategies;

namespace Gamess.Creatures
{
    /// <summary>
    /// Repræsenterer en fjende i spillet. Arver fra <see cref="Creature"/>.
    /// </summary>
    public class Enemy : Creature
    {
        public Enemy(string name, int hitPoints, IAttackStrategy strategy)
            : base(name, hitPoints, strategy)
        {
        }

        public override int GetEffectiveHit(Creature owner)
        {
            int baseHit = attackStrategy.CalculateDamage(attackItems);
            return baseHit;
        }
    }
}