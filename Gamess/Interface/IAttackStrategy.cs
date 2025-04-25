using Gamess.Items;

public interface IAttackStrategy
{
    int CalculateDamage(IEnumerable<AttackItem> items);
}

