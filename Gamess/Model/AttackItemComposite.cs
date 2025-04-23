using Gamess;
using Gamess.Creatures;
using Gamess.Items;

public class AttackItemComposite : AttackItem
{
    private List<AttackItem> _attackItems = new List<AttackItem>();

    public AttackItemComposite(string name)
        : base(name, 0, 0) { }

    public void Add(AttackItem attackItem)
    {
        _attackItems.Add(attackItem);
    }

    public void Remove(AttackItem attackItem)
    {
        _attackItems.Remove(attackItem);
    }

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