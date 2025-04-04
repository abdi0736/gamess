namespace Gamess;

public class AttackItem : WorldObject
{
    public int Hit { get; set; }
    public int Range { get; set; }

    public AttackItem(string name, int hit, int range)
        : base(name, true, false) // Lootable by default
    {
        Hit = hit;
        Range = range;
    }

}