namespace Gamess;

public abstract class Creature
{
    public string Name { get; set; }
    public int HitPoints { get; set; }

    protected IAttackStrategy attackStrategy;
    protected List<AttackItem> attackItems = new();
    protected List<DefenceItem> defenceItems = new();

    public Creature(string name, int hitPoints, IAttackStrategy strategy)
    {
        Name = name;
        HitPoints = hitPoints;
        attackStrategy = strategy;
    }

    /// <summary>
    /// Template Method - defines the steps for attacking another creature.
    /// </summary>
    public void PerformAttack(Creature target)
    {
        Console.WriteLine($"{Name} prepares an attack...");

        int damage = CalculateDamage(); // ↩️ implemented by subclasses
        Console.WriteLine($"{Name} attacks with {damage} damage!");

        target.ReceiveHit(damage);
    }

    /// <summary>
    /// Subclasses override this to provide their own attack calculation.
    /// </summary>
    protected abstract int CalculateDamage();

    /// <summary>
    /// Handles receiving damage.
    /// </summary>
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

        OnHit(); // Observer hook
    }

    /// <summary>
    /// Observer Pattern hook - notify observers on hit.
    /// </summary>
    protected virtual void OnHit()
    {
        // Kan overskrives i subklasser eller bruges med IObserver senere
    }

    public void Loot(WorldObject obj)
    {
        if (obj.Lootable)
        {
            Console.WriteLine($"{Name} looted {obj.Name}");

            if (obj is AttackItem attackItem)
                EquipAttackItem(attackItem);
            else if (obj is DefenceItem defenceItem)
                EquipDefenceItem(defenceItem);
        }
        else
        {
            Console.WriteLine($"{Name} cannot loot {obj.Name}");
        }
    }

    public void EquipAttackItem(AttackItem item) => attackItems.Add(item);

    public void EquipDefenceItem(DefenceItem item) => defenceItems.Add(item);
}
