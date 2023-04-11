namespace TileTest.TileSet
{
    public abstract class TileBase
    {
        public int XLocation { get; private set; }
        public int YLocation { get; private set; }

        public List<TileSide> Sides { get; private set; }

        protected List<string> _temp = new List<string>() { "Road" };

        protected List<string> _blank = new List<string>() { "Blank", "Road" };

        public TileBase(int xLocation, int yLocation)
        {
            XLocation = xLocation;
            YLocation = yLocation;

            Sides = new List<TileSide>();
            GenerateSideData();
        }

        protected abstract void GenerateSideData();
    }
}
