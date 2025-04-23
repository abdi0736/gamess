using System.Diagnostics;

namespace Gamess.Observers
{
    public class HitLogger : ICreatureObserver
    {
        public void OnCreatureHit(Creature creature, int damageTaken)
        {
            Trace.WriteLineIf(GameLogger.TraceSwitch.TraceInfo,
                $"[OBSERVE] {creature.Name} was hit for {damageTaken} damage. Remaining HP: {creature.HitPoints}");
        }
    }
}