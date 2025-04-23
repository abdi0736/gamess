using System;
using System.Diagnostics;
using Gamess.Creatures;
using Gamess.Items;
using Gamess.Strategies;
using Gamess.Observers;
using Gamess.World;

namespace Gamess
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the logger (Console and File outputs with "verbose" logging level)
            GameLogger.Initialize("Both", "verbose");

            #region Battle 1: Hero (Abdi) vs Orc
            // Create World
            World.World world = new World.World(25, 15);  // Example size, can be modified
            Trace.WriteLine($"World Size: {world.MaxX} x {world.MaxY}", "Info");

            // Setup hero creature with a strategy
            Creature hero = new Hero("Abdi", 100, new DefaultAttackStrategy());

            // Equip some attack items
            var sword = new AttackItem("Sword", 10, 1);
            sword.SetBoostFunction((owner, baseHit) =>
            {
                // Boost attack if health is below 25%
                if (owner.HitPoints < (owner.HitPoints * 0.25))
                {
                    return baseHit + 5;  // Boost by 5
                }
                return baseHit;  // No change
            });
            hero.EquipAttackItem(sword);
            
            var fireball = new AttackItem("Fireball", 20, 3);
            hero.EquipAttackItem(fireball);

            // Setup enemy creature
            Creature enemy = new Hero("Orc", 50, new DefaultAttackStrategy());

            // Add observer to enemy (e.g., logging when the enemy is hit)
            enemy.AddObserver(new HitLogger());

            // Show equipped items for hero
            Trace.WriteLine($"\n{hero.Name} has equipped the following attack items:", "Info");
            foreach (var item in hero.GetAttackItems())  // Using GetAttackItems method
            {
                Trace.WriteLine($"- {item.Name} (Damage: {item.Hit}, Range: {item.Range})", "Info");
            }

            // Print stats before battle
            Trace.WriteLine($"\n== Battle 1: Abdi vs Orc ==\n{hero.Name} HP: {hero.HitPoints} | {enemy.Name} HP: {enemy.HitPoints}", "Info");

            // Hero performs an attack
            hero.PerformAttack(enemy);

            // Print stats after battle
            Trace.WriteLine($"\n== After the attack ==\n{enemy.Name} HP: {enemy.HitPoints}", "Info");

            // Example of using the composite pattern for attack items
            AttackItemComposite compositeWeapon = new AttackItemComposite("Magic sword");
            compositeWeapon.Add(sword);
            compositeWeapon.Add(fireball);

            Trace.WriteLine($"\nHero now using a composite weapon: {compositeWeapon.Name}", "Info");
            Trace.WriteLine($"Total damage: {compositeWeapon.GetEffectiveHit(hero)}", "Info");

            // Final battle stats
            Trace.WriteLine($"\n== Final Battle ==\n{hero.Name} HP: {hero.HitPoints} | {enemy.Name} HP: {enemy.HitPoints}", "Info");
            #endregion

            #region Battle 2: Warrior (Liam) vs Goblin
            // Create Warrior and Goblin
            Creature warrior = new Hero("Liam", 80, new DefaultAttackStrategy());
            var axe = new AttackItem("Axe", 15, 1);
            var shield = new DefenceItem("Shield", 5);
            warrior.EquipAttackItem(axe);
            warrior.EquipDefenceItem(shield);

            Creature goblin = new Hero("Goblin", 40, new DefaultAttackStrategy());

            // Show equipped items for warrior
            Trace.WriteLine($"\n{warrior.Name} has equipped the following attack items:", "Info");
            foreach (var item in warrior.GetAttackItems())  // Using GetAttackItems method
            {
                Trace.WriteLine($"- {item.Name} (Damage: {item.Hit}, Range: {item.Range})", "Info");
            }

            // Print stats before battle
            Trace.WriteLine($"\n== Battle 2: Liam vs Goblin ==\n{warrior.Name} HP: {warrior.HitPoints} | {goblin.Name} HP: {goblin.HitPoints}", "Info");

            // Warrior performs an attack
            warrior.PerformAttack(goblin);

            // Print stats after battle
            Trace.WriteLine($"\n== After the attack ==\n{goblin.Name} HP: {goblin.HitPoints}", "Info");

            // Final battle stats
            Trace.WriteLine($"\n== Final Battle ==\n{warrior.Name} HP: {warrior.HitPoints} | {goblin.Name} HP: {goblin.HitPoints}", "Info");
            #endregion

            #region Battle 3: Archer (Sarah) vs Troll
            // Create Archer and Troll
            Creature archer = new Hero("Sarah", 90, new DefaultAttackStrategy());
            var bow = new AttackItem("Bow", 12, 3);
            archer.EquipAttackItem(bow);

            Creature troll = new Hero("Troll", 60, new DefaultAttackStrategy());

            // Show equipped items for archer
            Trace.WriteLine($"\n{archer.Name} has equipped the following attack items:", "Info");
            foreach (var item in archer.GetAttackItems())  // Using GetAttackItems method
            {
                Trace.WriteLine($"- {item.Name} (Damage: {item.Hit}, Range: {item.Range})", "Info");
            }

            // Print stats before battle
            Trace.WriteLine($"\n== Battle 3: Sarah vs Troll ==\n{archer.Name} HP: {archer.HitPoints} | {troll.Name} HP: {troll.HitPoints}", "Info");

            // Archer performs an attack
            archer.PerformAttack(troll);

            // Print stats after battle
            Trace.WriteLine($"\n== After the attack ==\n{troll.Name} HP: {troll.HitPoints}", "Info");

            // Final battle stats
            Trace.WriteLine($"\n== Final Battle ==\n{archer.Name} HP: {archer.HitPoints} | {troll.Name} HP: {troll.HitPoints}", "Info");
            #endregion
        }
    }
}
