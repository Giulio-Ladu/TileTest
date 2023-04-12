namespace TileTest.TileSet
{
    public class Cell
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public int Entropy 
        {
            get => PotentialTiles.Count; 
        }

        public List<TileBase> PotentialTiles { get; internal set; }

        public TileBase Tile { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;

            PotentialTiles = new List<TileBase>()
            {
                new BlankTile(x, y),
                new CurveTile(x, y),
                new LineTile(x, y),
                new TTile(x, y)
            };
        }

        internal void HandleCollapse(SideName side, string constraints)
        {
            if (PotentialTiles.Any())
            {
                var test = new List<TileBase>();

                switch (constraints)
                {
                    case "Road":
                    case "Blank":
                        // return all tile bases which have a blank tile in the same side as passed in
                        test = PotentialTiles.Where(z => z.Sides.FirstOrDefault(x => x.Side == side) != null).ToList();
                        break;
                        // return all tiles 
                    default:
                        test = PotentialTiles;
                        break;
                }

                PotentialTiles = test;
            }
        }
    }
}
