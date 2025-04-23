namespace Gamess.Items
{
    public class DefenceItem
    {
        public int ReduceHitPoint { get; set; }
        public string Name { get; set; }

        public DefenceItem(string name, int reduceHitPoint)
        {
            Name = name;
            ReduceHitPoint = reduceHitPoint;
        }
    }
}