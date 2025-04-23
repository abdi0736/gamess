using Gamess;
using Gamess.Creatures;
using Gamess.Items;
using System;
using System.Collections.Generic;

namespace Gamess.Items
{
    /// <summary>
    /// Repræsenterer et sammensat angrebsvåben, som kan indeholde flere angrebsvåben og beregne den samlede skade.
    /// Denne klasse implementerer <see cref="AttackItem"/> og tillader tilføjelse og fjernelse af individuelle angrebsvåben.
    /// </summary>
    public class AttackItemComposite : AttackItem
    {
        private List<AttackItem> _attackItems = new List<AttackItem>();

        /// <summary>
        /// Initialiserer et nyt sammensat angrebsvåben.
        /// </summary>
        /// <param name="name">Navnet på det sammensatte angrebsvåben.</param>
        public AttackItemComposite(string name)
            : base(name, 0, 0) { }

        /// <summary>
        /// Tilføjer et angrebsvåben til det sammensatte angrebsvåben.
        /// </summary>
        /// <param name="attackItem">Angrebsvåbenet, der skal tilføjes.</param>
        public void Add(AttackItem attackItem)
        {
            _attackItems.Add(attackItem);
        }

        /// <summary>
        /// Fjerner et angrebsvåben fra det sammensatte angrebsvåben.
        /// </summary>
        /// <param name="attackItem">Angrebsvåbenet, der skal fjernes.</param>
        public void Remove(AttackItem attackItem)
        {
            _attackItems.Remove(attackItem);
        }

        /// <summary>
        /// Beregner den samlede effektive skade for det sammensatte angrebsvåben ved at summere den effektive skade 
        /// fra hvert individuelt angrebsvåben, som er en del af det sammensatte våben.
        /// </summary>
        /// <param name="owner">Væsnet, der ejer det sammensatte angrebsvåben.</param>
        /// <returns>Den samlede effektive skade for det sammensatte angrebsvåben.</returns>
        public override int GetEffectiveHit(Creature owner)
        {
            int totalDamage = 0;
            foreach (var item in _attackItems)
            {
                totalDamage += item.GetEffectiveHit(owner);
            }
            return totalDamage;
        }
    }
}
