﻿namespace TileTest.TileSet
{
    public class LineTile : TileBase
    {
        public override List<string> DrawTile()
        {
            var result = new List<string>();

            result.Add("     ");
            result.Add("-----");
            result.Add("     ");

            return result;
        }

        protected override void GenerateSideData()
        {
            Sides.Add(new TileSide(SideName.Left, true, new List<TileTypes> { TileTypes.LineTile, TileTypes.TTile, TileTypes.CrossTile, TileTypes.CurveTile }));
            Sides.Add(new TileSide(SideName.Top, true, new List<TileTypes> { TileTypes.BlankTile }));
            Sides.Add(new TileSide(SideName.Right, true, new List<TileTypes> { TileTypes.LineTile, TileTypes.CrossTile, TileTypes.TTile, TileTypes.CurveTile }));
            Sides.Add(new TileSide(SideName.Bottom, true, new List<TileTypes> { TileTypes.BlankTile }));
        }
    }
}
