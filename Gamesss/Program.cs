using System;
using System.Diagnostics;
using Gamess.Creatures;
using Gamess.Items;
using Gamess.Strategies;
using Gamess.Observers;
using Gamess.World;
using Gamess.Config;

namespace Gamess
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration from XML
            GameConfig config = GameConfig.LoadFromFile("config.xml");

            // Initialize the logger
            GameLogger.Initialize(config.LogOutput, config.LogLevel);

            // Create World using config values
            World.World world = new World.World(config.MaxX, config.MaxY);
            Trace.WriteLine($"Verden størrelse: {world.MaxX} x {world.MaxY}", "Info");
            Trace.WriteLine($"Sværhedsgrad: {config.Level}", "Info");

            // === Hero setup ===
            Creature hero = new Hero("Abdi", 100, new DefaultAttackStrategy());

            var sword = new AttackItem("Sword", 10, 1);
            sword.SetBoostFunction((owner, baseHit) =>
                owner.HitPoints < (owner.HitPoints * 0.25) ? baseHit + 5 : baseHit
            );
            hero.EquipAttackItem(sword);

            var fireball = new AttackItem("Fireball", 20, 3);
            hero.EquipAttackItem(fireball);

            Creature enemy = new Hero("Orc", 50, new DefaultAttackStrategy());
            enemy.AddObserver(new HitLogger());

            // Display equipped items
            Trace.WriteLine($"\n{hero.Name} har følgende våben:", "Info");
            foreach (var item in hero.GetAttackItems())
            {
                Trace.WriteLine($"- {item.Name} (Damage: {item.Hit}, Range: {item.Range})", "Info");
            }

            Trace.WriteLine($"\n== Før kampen ==\n{hero.Name} HP: {hero.HitPoints} | {enemy.Name} HP: {enemy.HitPoints}", "Info");
            hero.PerformAttack(enemy);
            Trace.WriteLine($"\n== Efter angreb ==\n{enemy.Name} HP: {enemy.HitPoints}", "Info");

            // Composite weapon
            AttackItemComposite compositeWeapon = new AttackItemComposite("Magic Sword");
            compositeWeapon.Add(sword);
            compositeWeapon.Add(fireball);

            Trace.WriteLine($"\n{hero.Name} bruger nu: {compositeWeapon.Name}", "Info");
            Trace.WriteLine($"Total skade: {compositeWeapon.GetEffectiveHit(hero)}", "Info");
        }
    }
}
