using System.Diagnostics;
using TileTest.TileSet;

namespace TileTest
{
    public class GameBoard
    {
        public int Width { get; private set; }
        
        public int Height { get; private set; }

        private List<TileBase> _board;

        private List<TileTypes> _tiles;

        public GameBoard(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Invalid width or height defined, both values should be greater than '0'");
            }

            Width = width;
            Height = height;

            _board = new List<TileBase>();

            _tiles = new List<TileTypes>
            {
                TileTypes.T,
                TileTypes.Line,
                TileTypes.Curve,
                TileTypes.Blank
            };

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _board.Add(new BlankTile(i, j));
                }
            }

            GenerateTiles();
        }

        public void GenerateTiles()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    // Get the tile we are trying to define
                    var selectedTile = GetTile(x, y);

                    // Get all surrounding tiles 
                    
                    // Left
                    var left = GetTile(x, y - 1);

                    // Right
                    var right = GetTile(x, y + 1);

                    // Up 
                    var top = GetTile(x - 1, y);

                    // Down
                    var bottom = GetTile(x + 1, y);

                    var blank = "Blank";
                    var potentialLeft = blank;
                    var potentialRight = blank;
                    var potentialBottom = blank;
                    var potentialTop = blank;

                    // Find the next tile based on which sides are visible
                    if (left != null)
                    {
                        // Get the left right side of the left hand tile
                        var rightSide = left.Sides.FirstOrDefault(x => x.Side == SideName.Right);
                        if (rightSide != null)
                        {
                            potentialLeft = rightSide.GetRandomTileSet();
                        }
                    }

                    if (right != null)
                    {
                        // Get the left right side of the left hand tile
                        var leftSide = right.Sides.FirstOrDefault(x => x.Side == SideName.Left);
                        if (leftSide != null)
                        {
                            potentialRight = leftSide.GetRandomTileSet();
                        }
                    }

                    if (top != null)
                    {
                        // Get the left right side of the left hand tile
                        var bottomSide = top.Sides.FirstOrDefault(x => x.Side == SideName.Bottom);
                        if (bottomSide != null)
                        {
                            potentialBottom = bottomSide.GetRandomTileSet();
                        }
                    }

                    if (bottom != null)
                    {
                        // Get the left right side of the left hand tile
                        var topSide = bottom.Sides.FirstOrDefault(x => x.Side == SideName.Top);
                        if (topSide != null)
                        {
                            potentialTop = topSide.GetRandomTileSet();
                        }
                    }



                    Debug.WriteLine("Looking for a tile with the following attributes :" +potentialLeft + ", "+potentialRight+", "+potentialTop+", "+potentialBottom);
                }
            }
            
            Debug.WriteLine("Getting here");
        }

        private TileBase GetTile(int x, int y)
        {
            return _board.FirstOrDefault(tile => tile.XLocation == x && tile.YLocation == y);
        }

        private TileBase GetRandomTileFromTileSet(int x, int y)
        {
            var tiles = Enum.GetValues(typeof(TileTypes));
            var random = new Random();
            TileTypes selected = (TileTypes)tiles.GetValue(random.Next(tiles.Length));

            switch (selected) 
            {
                case TileTypes.T:
                    return new TTile(x, y);
                case TileTypes.Line:
                    return new LineTile(x, y);
                case TileTypes.Curve:
                    return new CurveTile(x, y);
                default:
                    return new BlankTile(x, y);
            }
        }
    }
}
