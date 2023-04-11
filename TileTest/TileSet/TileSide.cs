namespace TileTest.TileSet
{
    public class TileSide
    {
        public SideName Side { get; private set; }

        public bool Connectable { get; private set; }

        public List<string> PossibleConnections { get; private set; }

        public TileSide(SideName side, bool connectable, List<string> possibleConnections)
        {
            Side = side;
            Connectable = connectable;
            PossibleConnections = possibleConnections ?? throw new ArgumentNullException(nameof(possibleConnections));
        }

        public string GetRandomTileSet()
        {
            if (PossibleConnections.Count > 0)
            {
                var rnd = new Random((int)DateTime.Now.Ticks);
                return PossibleConnections[rnd.Next(PossibleConnections.Count)];
            }

            return string.Empty;
        }
    }
}
