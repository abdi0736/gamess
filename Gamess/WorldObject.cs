namespace Gamess;

public abstract class WorldObject
{
    public string Name { get; set; }
    public bool Lootable { get; set; }
    public bool Removable { get; set; }

    protected WorldObject(string name, bool lootable, bool removable)
    {
        Name = name;
        Lootable = lootable;
        Removable = removable;
    }
} 