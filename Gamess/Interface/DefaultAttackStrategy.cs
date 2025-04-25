using System.Collections.Generic;
using System.Linq;
using Gamess.Creatures;
using Gamess.Items;

namespace Gamess.Strategies
{
    /// <summary>
    /// A default implementation of IAttackStrategy.
    /// Sums up the total damage from all attack items.
    /// </summary>
    public class DefaultAttackStrategy : IAttackStrategy
    {
        public int CalculateDamage(IEnumerable<AttackItem> items)
        {
            return items.Sum(i => i.Hit);
        }
    }
}