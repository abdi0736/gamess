namespace Gamess.World
{
    public class World
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        // Constructor to initialize world size
        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}