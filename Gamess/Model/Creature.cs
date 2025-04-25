using System;
using System.Collections.Generic;
using System.Linq;
using Gamess.Items;
using Gamess.Observers;
using Gamess.Strategies;

namespace Gamess.Creatures
{
    /// <summary>
    /// Abstract base class representing a creature in the game world.
    /// Applies Template Method, Strategy, and Observer design patterns.
    /// Demonstrates SOLID principles: S, O, L, I.
    /// </summary>
    public abstract class Creature
    {
        /// <summary>
        /// Name of the creature.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current hit points of the creature.
        /// </summary>
        public int HitPoints { get; set; }

        /// <summary>
        /// Maximum hit points the creature can have.
        /// </summary>
        public int MaxHitPoints { get; set; }

        protected IAttackStrategy attackStrategy;
        protected List<AttackItem> attackItems = new();
        protected List<DefenceItem> defenceItems = new();
        private List<ICreatureObserver> observers = new();

        /// <summary>
        /// Constructor for Creature.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="hitPoints">The initial hit points of the creature.</param>
        /// <param name="strategy">The attack strategy used by the creature (Strategy Pattern).</param>
        public Creature(string name, int hitPoints, IAttackStrategy strategy)
        {
            Name = name;
            HitPoints = hitPoints;
            MaxHitPoints = hitPoints;
            attackStrategy = strategy;
        }

        /// <summary>
        /// Adds an observer to be notified when this creature is hit. (Observer Pattern)
        /// </summary>
        public void AddObserver(ICreatureObserver observer) => observers.Add(observer);

        /// <summary>
        /// Removes an observer.
        /// </summary>
        public void RemoveObserver(ICreatureObserver observer) => observers.Remove(observer);

        /// <summary>
        /// Equips an attack item to the creature.
        /// </summary>
        public void EquipAttackItem(AttackItem item) => attackItems.Add(item);

        /// <summary>
        /// Equips a defence item to the creature.
        /// </summary>
        public void EquipDefenceItem(DefenceItem item) => defenceItems.Add(item);

        /// <summary>
        /// Gets the list of attack items.
        /// </summary>
        public List<AttackItem> GetAttackItems() => attackItems;

        /// <summary>
        /// Executes the attack sequence using the Template Method pattern.
        /// Calls GetEffectiveHit which can be overridden in derived classes.
        /// </summary>
        public void PerformAttack(Creature target)
        {
            Console.WriteLine($"{Name} prepares an attack...");
            int damage = GetEffectiveHit(target); // Template method call
            Console.WriteLine($"{Name} attacks with {damage} damage!");
            target.ReceiveHit(damage);
        }

        /// <summary>
        /// Handles receiving a hit, calculating damage reduction and notifying observers.
        /// Uses LINQ to calculate defense. Applies Observer Pattern.
        /// </summary>
        public void ReceiveHit(int hit)
        {
            int totalDefense = defenceItems.Sum(d => d.ReduceHitPoint); // LINQ
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

        /// <summary>
        /// Computes the effective hit amount based on the creature’s attack strategy.
        /// Can be overridden in subclasses for custom logic. (Template + Strategy)
        /// </summary>
        public virtual int GetEffectiveHit(Creature owner)
        {
            return attackStrategy.CalculateDamage(attackItems); // Strategy pattern
        }
    }
}
