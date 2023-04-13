namespace TileTest.TileSet
{
    public class BlankTile : TileBase
    {
        public override List<string> DrawTile()
        {
            var result = new List<string>();

            result.Add("     ");
            result.Add("     ");
            result.Add("     ");

            return result;
        }

        protected override void GenerateSideData()
        {
            Sides.Add(new TileSide(SideName.Left, true, new List<TileTypes> { TileTypes.BlankTile }));
            Sides.Add(new TileSide(SideName.Top, true, new List<TileTypes> { TileTypes.BlankTile, TileTypes.LineTile, TileTypes.TTile, TileTypes.MidTile }));
            Sides.Add(new TileSide(SideName.Right, true, new List<TileTypes> { TileTypes.BlankTile, TileTypes.MidTile }));
            Sides.Add(new TileSide(SideName.Bottom, true, new List<TileTypes> { TileTypes.BlankTile, TileTypes.CurveTile, TileTypes.LineTile, TileTypes.MidTile }));
        }
    }
}
