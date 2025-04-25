using System;
using System.Diagnostics;
using Gamess.Config;
using Gamess.Creatures;
using Gamess.Items;
using Gamess.Strategies;
using Gamess.Observers;
using Gamess.Model; // <- Dette er korrekt namespace hvor Config ligger


namespace Gamess
{
    class Program
    {
        static void Main(string[] args)
        {
            GameConfig config = GameConfig.LoadFromFile("config.xml");  
            GameLogger.Initialize(config.LogOutput, config.LogLevel);

            // Create World using config values
            World.World world = new World.World(config.MaxX, config.MaxY);
            Trace.WriteLine($"Verden størrelse: {world.MaxX} x {world.MaxY}", "Info");
            Trace.WriteLine($"Sværhedsgrad: {config.Level}", "Info");


            // === Hero setup ===
            var hero = new Hero("Abdi", 100, new DefaultAttackStrategy());

            var sword = new AttackItem("Sword", 10, 1);
            sword.SetBoostFunction((owner, baseHit) =>
                owner.HitPoints < (owner.MaxHitPoints * 0.25) ? baseHit + 5 : baseHit
            );

            var fireball = new AttackItem("Fireball", 20, 2);

            hero.EquipAttackItem(sword);
            hero.EquipAttackItem(fireball);

            // === Enemy setup ===
            var enemy = new Hero("Goblin", 80, new DefaultAttackStrategy());
            enemy.EquipAttackItem(new AttackItem("Claws", 8, 1));
            enemy.AddObserver(new HitLogger());

            Trace.WriteLine($"\n{hero.Name} vs. {enemy.Name} starter nu!");

            // === Kamp loop ===
            int round = 1;
            while (hero.HitPoints > 0 && enemy.HitPoints > 0)
            {
                Trace.WriteLine($"\n--- Runde {round} ---");

                hero.PerformAttack(enemy);
                if (enemy.HitPoints <= 0) break;

                enemy.PerformAttack(hero);
                round++;
            }

            // === Vinder ===
            string winner = hero.HitPoints > 0 ? hero.Name : enemy.Name;
            Trace.WriteLine($"\n== Kamp slut ==\nVinder: {winner}", "Info");

            // === Composite våben test ===
            var composite = new AttackItemComposite("Magic Sword");
            composite.Add(sword);
            composite.Add(fireball);

            Trace.WriteLine($"\n{hero.Name} bruger nu {composite.Name} med skade: {composite.GetEffectiveHit(hero)}");
        }
    }
}
