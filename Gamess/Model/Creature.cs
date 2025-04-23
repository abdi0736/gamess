using Gamess.Items;
using Gamess.Observers;
using Gamess.Strategies;

public abstract class Creature
{
    public string Name { get; set; }
    public int HitPoints { get; set; }
    public int MaxHitPoints { get; set; }  // Add this property to hold the maximum hit points

    protected IAttackStrategy attackStrategy;
    protected List<AttackItem> attackItems = new();
    protected List<DefenceItem> defenceItems = new();

    private List<ICreatureObserver> observers = new();

    public Creature(string name, int hitPoints, IAttackStrategy strategy)
    {
        Name = name;
        HitPoints = hitPoints;
        MaxHitPoints = hitPoints;  // Set MaxHitPoints to the initial value of HitPoints
        attackStrategy = strategy;
    }

    public void AddObserver(ICreatureObserver observer) => observers.Add(observer);
    public void RemoveObserver(ICreatureObserver observer) => observers.Remove(observer);

    public void EquipAttackItem(AttackItem item) => attackItems.Add(item);
    public void EquipDefenceItem(DefenceItem item) => defenceItems.Add(item);

    // Offentlig metode til at få attackItems
    public List<AttackItem> GetAttackItems() => attackItems;

    // Make GetEffectiveHit a virtual method so it can be overridden in derived classes
    public virtual int GetEffectiveHit(Creature owner)
    {
        // Base implementation if needed, or return 0 if not implemented here
        return 0;
    }

    public void PerformAttack(Creature target)
    {
        Console.WriteLine($"{Name} prepares an attack...");
        int damage = GetEffectiveHit(target); // Now using GetEffectiveHit instead of CalculateDamage
        Console.WriteLine($"{Name} attacks with {damage} damage!");
        target.ReceiveHit(damage);
    }

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
}
