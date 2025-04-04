namespace Gamess;

public interface IAttackStrategy
{
    int CalculateAttack(List<AttackItem> items);
}