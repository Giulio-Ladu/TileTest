using System.Diagnostics;
using TileTest.TileSet;

namespace TileTest
{
    public class GameBoard
    {
        public int Width { get; private set; }
        
        public int Height { get; private set; }

        private List<Cell> _board;

        private List<TileBase> _tilesBucket;

        public GameBoard(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Invalid width or height defined, both values should be greater than '0'");
            }

            Width = width;
            Height = height;

            _board = new List<Cell>();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++) 
                {
                    _board.Add(new Cell(j, i));
                }
            }

            Collapse(1,0);

            Debug.WriteLine("Getting here");
        }

        private void Collapse(int x, int y)
        {
            // Get number of cells with entropy > 1
            // Continue loop whilst this is greater than 1

            // Pick a random tile and see if you can reduce the entropy, by looking at the tiles next to it

            var selectedCell = _board.FirstOrDefault(c => c.X == x && c.Y == y);
            
            if (selectedCell != null)
            {
                // -------------------------------------------------------------------
                // TEST ONLY 
                selectedCell.Tile = new LineTile(x, y);
                // -------------------------------------------------------------------

                // get surrounded cells
                var leftCell = _board.FirstOrDefault(c => c.X == x - 1 && c.Y == y);
                var rightCell = _board.FirstOrDefault(c => c.X == x + 1 && c.Y == y);
                var topCell = _board.FirstOrDefault(c => c.X == x && c.Y == y - 1);
                var bottomCell = _board.FirstOrDefault(c => c.X == x && c.Y == y + 1);

                // if we have a tile on the left, we can check if it has a tile attached
                if (leftCell != null)
                {
                    if (leftCell.Tile != null)
                    {
                        // we have a tile in the left hand side and we may be able to reduce our entropy
                        var leftCellRightEdge = leftCell.Tile.Sides.FirstOrDefault(f => f.Side == SideName.Right);

                        if (leftCellRightEdge != null)
                        {
                            // Get the edge of the cells tile and see what potentials we have
                            if (leftCellRightEdge.Connectable)
                            {
                                var partentials = leftCellRightEdge.PossibleConnections.ToList();
                            }
                        }
                    }
                    else
                    {
                        // Nothing in the neighbouring cell so that side can be anything
                        selectedCell.HandleCollapse(SideName.Left, "Any");
                    }
                }

                Debug.WriteLine("TEST");
            }
        }
    }
}
