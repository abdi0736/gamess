using System;
using System.Diagnostics;
using Gamess;
using Gamess.Config;
using Gamess.Creatures;
using Gamess.Items;
using Gamess.Strategies;
using Gamess.Observers;
using Gamess.Model;
using Gamess.world;
using Gamess.World;

namespace FrameworkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GameConfig config = GameConfig.LoadFromFile("config.xml");  
            GameLogger.Initialize(config.LogOutput, config.LogLevel);

            // Create World using config values
            World world = new World(config.MaxX, config.MaxY);
            Trace.WriteLine($"Verden størrelse: {world.MaxX} x {world.MaxY}", "Info");
            Trace.WriteLine($"Sværhedsgrad: {config.Level}", "Info");


            // === Create a hero with attack strategy ===
            var hero = new Hero("TestHero", 100, new DefaultAttackStrategy());
            var sword = new AttackItem("Basic Sword", 15, 1);
            var fireball = new AttackItem("Fireball", 25, 2);

            hero.EquipAttackItem(sword);
            hero.EquipAttackItem(fireball);

            // === Create an enemy with observer ===
            var enemy = new Enemy("testEnemy", 80, new DefaultAttackStrategy());
            enemy.EquipAttackItem(new AttackItem("Claws", 10, 1));
            enemy.AddObserver(new HitLogger());

            // === Demonstrate combat ===
            Trace.WriteLine($"\n{hero.Name} attacks {enemy.Name}!");
            hero.PerformAttack(enemy);

            Trace.WriteLine($"{enemy.Name} attacks {hero.Name}!");
            enemy.PerformAttack(hero);

            // === Composite weapon test ===
            var comboWeapon = new AttackItemComposite("Ultimate Combo");
            comboWeapon.Add(sword);
            comboWeapon.Add(fireball);

            int damage = comboWeapon.GetEffectiveHit(hero);
            Trace.WriteLine($"\n{hero.Name} uses composite weapon '{comboWeapon.Name}' for {damage} damage!");

            // === Done ===
            Trace.WriteLine("\nFramework test completed!");
        }
    }
}
