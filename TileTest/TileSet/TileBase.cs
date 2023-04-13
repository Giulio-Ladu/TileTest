namespace TileTest.TileSet
{
    public abstract class TileBase
    {
        public List<TileSide> Sides { get; private set; }

        public TileBase()
        {
            Sides = new List<TileSide>();
            GenerateSideData();
        }

        protected abstract void GenerateSideData();

        public abstract List<string> DrawTile();
    }
}
