using System;
using Gamess.Creatures;

namespace Gamess.Items
{
    public class AttackItem : WorldObject
    {
        public int Hit { get; set; }
        public int Range { get; set; }

        // En valgfri boost delegate, der kan bruges til at ændre skaden
        public Func<Creature, int, int>? BoostFunction { get; set; }

        public AttackItem(string name, int hit, int range)
            : base(name, lootable: true, removable: false)
        {
            Hit = hit;
            Range = range;
        }

        // Make GetEffectiveHit a virtual method so it can be overridden in derived classes
        public virtual int GetEffectiveHit(Creature owner)
        {
            int effectiveHit = Hit;  // Start med den normale skadeværdi

            // Hvis BoostFunction er defineret, anvend den på skaden
            if (BoostFunction != null)
            {
                effectiveHit = BoostFunction(owner, effectiveHit);
            }

            return effectiveHit;
        }

        // En simpel metode til at ændre boostet dynamisk
        public void SetBoostFunction(Func<Creature, int, int> boostFunction)
        {
            BoostFunction = boostFunction;
        }
    }
}