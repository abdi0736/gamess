using System;
using System.Collections.Generic;
using System.Linq;
using Gamess.Items;
using Gamess.Observers;
using Gamess.Strategies;

namespace Gamess.Creatures
{
    public abstract class Creature
    {
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }

        protected IAttackStrategy attackStrategy;
        protected List<AttackItem> attackItems = new();
        protected List<DefenceItem> defenceItems = new();
        private List<ICreatureObserver> observers = new();

        public Creature(string name, int hitPoints, IAttackStrategy strategy)
        {
            Name = name;
            HitPoints = hitPoints;
            MaxHitPoints = hitPoints;
            attackStrategy = strategy;
        }

        // Observer pattern
        public void AddObserver(ICreatureObserver observer) => observers.Add(observer);
        public void RemoveObserver(ICreatureObserver observer) => observers.Remove(observer);

        // Udstyr
        public void EquipAttackItem(AttackItem item) => attackItems.Add(item);
        public void EquipDefenceItem(DefenceItem item) => defenceItems.Add(item);

        public List<AttackItem> GetAttackItems() => attackItems;

        // Template Method + Strategy
        public void PerformAttack(Creature target)
        {
            Console.WriteLine($"{Name} prepares an attack...");
            int damage = GetEffectiveHit(target); // Brug virtuel metode
            Console.WriteLine($"{Name} attacks with {damage} damage!");
            target.ReceiveHit(damage);
        }

        // Template Method + LINQ + Observer
        public void ReceiveHit(int hit)
        {
            int totalDefense = defenceItems.Sum(d => d.ReduceHitPoint);
            int netDamage = Math.Max(0, hit - totalDefense);

            HitPoints -= netDamage;
            if (HitPoints <= 0)
            {
                HitPoints = 0;
                Console.WriteLine($"{Name} has died.");
            }
            else
            {
                Console.WriteLine($"{Name} receives {netDamage} damage (reduced by {totalDefense}). Remaining HP: {HitPoints}");
            }

            foreach (var observer in observers)
            {
                observer.OnCreatureHit(this, netDamage);
            }
        }

        // Strategy eller virtuel logik for composite item damage
        public virtual int GetEffectiveHit(Creature owner)
        {
            return 0; // Kan override i Hero, Enemy, Boss etc.
        }
    }
}
