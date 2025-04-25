using Gamess.Creatures;

namespace Gamess.Strategies
{
    public interface IAttackStrategy
    {
        int CalculateDamage(Creature creature);
    }
}