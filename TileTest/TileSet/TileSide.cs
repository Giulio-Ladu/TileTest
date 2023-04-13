namespace TileTest.TileSet
{
    public class TileSide
    {
        public SideName Side { get; private set; }

        public bool Connectable { get; private set; }

        public List<TileTypes> PossibleConnections { get; private set; }

        public TileSide(SideName side, bool connectable, List<TileTypes> possibleConnections)
        {
            Side = side;
            Connectable = connectable;
            PossibleConnections = possibleConnections ?? throw new ArgumentNullException(nameof(possibleConnections));
        }

        public string GetRandomTileSet()
        {
            throw new NotImplementedException();
        }
    }
}
