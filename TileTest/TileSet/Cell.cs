namespace TileTest.TileSet
{
    public class Cell
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        //public int Entropy 
        //{
        //    get => PotentialTiles.Count; 
        //}

        //public List<TileTypes> PotentialTiles { get; internal set; } 

        public TileTypes Tile { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;

            Tile = TileTypes.NotDefined;

            //PotentialTiles = new List<TileTypes>(Enum.GetValues(typeof(TileTypes)).Cast<TileTypes>());
            //PotentialTiles.Remove(TileTypes.NotDefined);
        }
    }
}
