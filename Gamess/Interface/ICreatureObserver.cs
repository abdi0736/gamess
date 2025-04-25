using Gamess.Creatures;

namespace Gamess.Observers
{
    public interface ICreatureObserver
    {
        void OnCreatureHit(Creature creature, int damageTaken);
    }
}