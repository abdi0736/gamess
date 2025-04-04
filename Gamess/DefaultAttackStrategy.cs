namespace Gamess;

    public class DefaultAttackStrategy
    {
        public int CalculateAttack(List<AttackItem> items)
        {
            int total = items.Sum(a => a.Hit);
            return total > 0 ? total : new Random().Next(5, 15); // Basic attack with randomness
        }
    }


