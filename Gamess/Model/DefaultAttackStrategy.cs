using Gamess.Creatures;

namespace Gamess.Strategies
{
    public class DefaultAttackStrategy : IAttackStrategy
    {
        public int CalculateDamage(Creature creature)
        {
            return 10;  // Default attack damage
        }
    }
}