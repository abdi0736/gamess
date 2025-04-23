using System;
using Gamess.Creatures;

namespace Gamess.Items
{
    /// <summary>
    /// Repræsenterer et angrebsvåben, der kan bruges af væsner til at påføre skade.
    /// </summary>
    public class AttackItem : WorldObject
    {
        /// <summary>
        /// Får eller sætter de grundlæggende angrebspunkter for angrebsvåbenet.
        /// </summary>
        public int Hit { get; set; }

        /// <summary>
        /// Får eller sætter rækkevidden af angrebsvåbenet.
        /// </summary>
        public int Range { get; set; }

        /// <summary>
        /// En delegate, der kan bruges til at booste angrebspunkterne for angrebsvåbenet baseret på væsnets tilstand.
        /// </summary>
        public Func<Creature, int, int>? BoostFunction { get; set; }

        /// <summary>
        /// Initialiserer en ny instans af <see cref="AttackItem"/>-klassen med det angivne navn, angrebspunkter og rækkevidde.
        /// </summary>
        /// <param name="name">Navnet på angrebsvåbenet.</param>
        /// <param name="hit">De grundlæggende angrebspunkter for angrebsvåbenet.</param>
        /// <param name="range">Rækkevidden af angrebsvåbenet.</param>
        public AttackItem(string name, int hit, int range)
            : base(name, lootable: true, removable: false)
        {
            Hit = hit;
            Range = range;
        }

        /// <summary>
        /// Får den effektive angrebsværdi for angrebsvåbenet, og anvender eventuelle boostfunktioner, hvis de er defineret.
        /// </summary>
        /// <param name="owner">Væsnet, der ejer angrebsvåbenet.</param>
        /// <returns>Den effektive angrebsværdi efter eventuelle boost.</returns>
        public virtual int GetEffectiveHit(Creature owner)
        {
            int effectiveHit = Hit;  // Start med den normale angrebsværdi

            // Hvis en BoostFunction er defineret, anvendes den på angrebsværdien
            if (BoostFunction != null)
            {
                effectiveHit = BoostFunction(owner, effectiveHit);
            }

            return effectiveHit;
        }

        /// <summary>
        /// Sætter en ny boostfunktion, som vil blive brugt til at ændre angrebsvåbenets angrebsværdi.
        /// </summary>
        /// <param name="boostFunction">En delegate, der tager ejer-væsnet og den grundlæggende angrebsværdi og returnerer den ændrede angrebsværdi.</param>
        public void SetBoostFunction(Func<Creature, int, int> boostFunction)
        {
            BoostFunction = boostFunction;
        }
    }
}
