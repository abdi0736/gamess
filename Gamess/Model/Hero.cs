using Gamess.Strategies;

namespace Gamess.Creatures
{
    /// <summary>
    /// Repræsenterer en helt (Hero), som er en type væsen med angreb og specifik strategi.
    /// Denne klasse arver fra <see cref="Creature"/> og tilpasser den effektive skadeberegning.
    /// </summary>
    public class Hero : Creature
    {
        /// <summary>
        /// Initialiserer en ny helt med et navn, livspunkter og en angrebsstrategi.
        /// </summary>
        /// <param name="name">Navnet på helten.</param>
        /// <param name="hitPoints">Heltenes livspunkter.</param>
        /// <param name="strategy">Den strategi, som helten bruger til angreb.</param>
        public Hero(string name, int hitPoints, IAttackStrategy strategy) 
            : base(name, hitPoints, strategy)
        {
        }

        /// <summary>
        /// Beregner den effektive skade for helten ved at summere skaden fra alle udstyrende angrebsvåben.
        /// </summary>
        /// <param name="owner">Væsnet, der ejer helten.</param>
        /// <returns>Den samlede effektive skade for helten.</returns>
        public override int GetEffectiveHit(Creature owner)
        {
            int totalDamage = 0;

            // Iterér gennem de udstyrende angrebsvåben og beregn den samlede skade
            foreach (var item in attackItems)  // 'attackItems' kommer fra basisklassen
            {
                totalDamage += item.GetEffectiveHit(owner);  // Kald GetEffectiveHit for hvert angrebsvåben
            }

            return totalDamage;
        }
    }
}