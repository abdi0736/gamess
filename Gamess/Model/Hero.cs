using Gamess.Strategies;

namespace Gamess.Creatures
{
    public class Hero : Creature
    {
        public Hero(string name, int hitPoints, IAttackStrategy strategy) 
            : base(name, hitPoints, strategy)
        {
        }

        public override int GetEffectiveHit(Creature owner)
        {
            int totalDamage = 0;

            // Iterate over the equipped attack items and calculate the total damage
            foreach (var item in attackItems)  // 'attackItems' is from the base class
            {
                totalDamage += item.GetEffectiveHit(owner);  // Call GetEffectiveHit for each attack item
            }

            return totalDamage;
        }
    }
}