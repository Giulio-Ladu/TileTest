namespace TileTest.TileSet
{
    public class CurveTile : TileBase
    {
        public CurveTile(int xLocation, int yLocation) : base(xLocation, yLocation)
        {
            
        }

        protected override void GenerateSideData()
        {
            Sides.Add(new TileSide(SideName.Left, true, _temp));
            Sides.Add(new TileSide(SideName.Top, true, _temp));
            Sides.Add(new TileSide(SideName.Right, true, _blank));
            Sides.Add(new TileSide(SideName.Bottom, true, _blank));
            Sides.Add(new TileSide(SideName.Front, true, _blank));
            Sides.Add(new TileSide(SideName.Back, true, _blank));
        }
    }
}
