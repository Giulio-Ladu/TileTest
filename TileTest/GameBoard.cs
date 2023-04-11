using System.Diagnostics;
using TileTest.TileSet;

namespace TileTest
{
    public class GameBoard
    {
        public int Width { get; private set; }
        
        public int Height { get; private set; }

        private List<TileBase> _board;

        private List<TileBase> _tilesBucket;

        public GameBoard(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Invalid width or height defined, both values should be greater than '0'");
            }

            Width = width;
            Height = height;

            _board = new List<TileBase>();

            _tilesBucket = new List<TileBase>()
            {
                new BlankTile(0,0),
                new CurveTile(0,0),
                new BlankTile(0,0),
                new LineTile(0,0),
                new TTile(0,0)
            };

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _board.Add(new BlankTile(j, i));
                }
            }

            GenerateTiles();
        }

        public void GenerateTiles()
        {
            var xIndex = 0;
            var yIndex = 0;

            var startingTile = GetTile(0, 0);
            _board.Remove(startingTile);
            _board.Insert(0, new LineTile(xIndex, yIndex));
            xIndex++;

            for (int y = yIndex; y < Height; y++)
            {
                for (int x = xIndex; x < Width; x++)
                {
                    var selectedTile = GetTile(x, y);

                    if (selectedTile != null)
                    {
                        // Get surrounding tiles

                        // Left tile 
                        var leftTile = GetTile(x - 1, y);

                        // Right tile 
                        var rightTile = GetTile(x + 1, y);

                        // Top tile 
                        var topTile = GetTile(x, y + 1);

                        // Bottom tile 
                        var bottomTile = GetTile(x, y - 1);

                        if (leftTile != null)
                        {
                            // we have a left tile and so what constraints do I have?
                            var rightTest = leftTile.Sides.FirstOrDefault(s => s.Side == SideName.Right);

                            if (rightTest != null)
                            {
                                if (rightTest.Connectable)
                                {
                                    var test = rightTest.PossibleConnections;
                                }
                            }
                        }
                    }
                }
            }
        }

        private TileBase GetTile(int x, int y)
        {
            return _board.FirstOrDefault(tile => tile.XLocation == x && tile.YLocation == y);
        }
    }
}
