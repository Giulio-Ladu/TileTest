using Microsoft.VisualBasic;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text;
using TileTest.TileSet;

namespace TileTest
{
    public class GameBoard
    {
        public int Width { get; private set; }
        
        public int Height { get; private set; }

        private Cell[,] _board;

        public GameBoard(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Invalid width or height defined, both values should be greater than '0'");
            }

            Width = width;
            Height = height;

            _board = new Cell[Width, Height];

            GenerateBlankBoard();
            PopulateBoard();

            DrawBoard();

            Debug.WriteLine("------- Complete -------");
        }

        private void DrawBoard()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i <= Height - 1; i++)
            {
                StringBuilder lineOne = new StringBuilder();
                StringBuilder lineTwo = new StringBuilder();
                StringBuilder lineThree = new StringBuilder();

                for (int j = 0; j <= Width - 1; j++)
                {
                    var cell = _board[i, j];
                    var tile = GetTileByType(cell.Tile);
                    var lines = tile.DrawTile();

                    lineOne.Append("|");
                    lineOne.Append(lines[0]);
                    lineOne.Append("|");

                    lineTwo.Append("|");
                    lineTwo.Append(lines[1]);
                    lineTwo.Append("|");

                    lineThree.Append("|");
                    lineThree.Append(lines[2]);
                    lineThree.Append("|");
                }

                result.AppendLine(lineOne.ToString());
                result.AppendLine(lineTwo.ToString());
                result.AppendLine(lineThree.ToString());
            }

            Console.WriteLine(result.ToString());
        }

        private void PopulateBoard()
        {
            int counter = Width * Height;

            // Should use a latch for stopping loop
            while (counter > 0)
            {
                // Get the first cell
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var selectedCell = GetCell(x, y);

                        // If we do not have a tile defined
                        if (selectedCell?.Tile == TileTypes.NotDefined)
                        {
                            // Get surrounding tiles 
                            var tileTypesValues = Enum.GetValues(typeof(TileTypes)).Cast<TileTypes>().ToList();

                            var leftCell = GetCell(x - 1, y);
                            var topCell = GetCell(x, y - 1);
                            var rightCell = GetCell(x + 1, y);
                            var bottomCell = GetCell(x, y + 1);

                            var rnd = new Random();

                            // If surrounding tiles are all null or not defined, Pick a random tile and move on
                            if (!IsCellDefined(leftCell) && !IsCellDefined(topCell) 
                                && !IsCellDefined(rightCell) && !IsCellDefined(bottomCell))
                            {
                                TileTypes randomType = (TileTypes)rnd.Next(tileTypesValues.Count() -1);

                                selectedCell.Tile = randomType;
                            }
                            else
                            {
                                List<TileTypes> possibilities = tileTypesValues ?? new List<TileTypes>();

                                CollapsePosibilities(GetTilePossibilities(leftCell, SideName.Right), ref possibilities);
                                CollapsePosibilities(GetTilePossibilities(rightCell, SideName.Left), ref possibilities);
                                CollapsePosibilities(GetTilePossibilities(topCell, SideName.Bottom), ref possibilities);
                                CollapsePosibilities(GetTilePossibilities(bottomCell, SideName.Top), ref possibilities);

                                if (possibilities.Count > 0)
                                {
                                    var index = rnd.Next(possibilities.Count());
                                    TileTypes randomType = possibilities[index];
                                    selectedCell.Tile = randomType;
                                }
                                else
                                {
                                    // should we add a default
                                    selectedCell.Tile = TileTypes.BlankTile;
                                }

                                _board[y, x] = selectedCell;
                            }
                        }
                    }
                }

                counter--;
            }
        }

        private void CollapsePosibilities(List<TileTypes> types, ref List<TileTypes> posibilities)
        {
            if (types.Any())
            {
                posibilities = posibilities.Intersect(types).ToList();
            }
        }

        private List<TileTypes> GetTilePossibilities(Cell? cell, SideName side)
        {
            var result = new List<TileTypes>();

            if (IsCellDefined(cell))
            {
                var cellType = cell?.Tile;
                TileBase? tileTemplate = GetTileByType(cellType.Value);

                var sideData = tileTemplate?.Sides.FirstOrDefault(x => x.Side == side);

                if (sideData != null)
                {
                    result = sideData.PossibleConnections;
                }
            }

            return result;
        }

        private TileBase GetTileByType(TileTypes type)
        {
            TileBase tileTemplate;

            switch (type)
            {
                case TileTypes.LineTile:
                    tileTemplate = new LineTile();
                    break;
                case TileTypes.CrossTile:
                    tileTemplate = new CrossTile();
                    break;
                case TileTypes.CurveTile:
                    tileTemplate = new CurveTile();
                    break;
                case TileTypes.TTile:
                    tileTemplate = new TTile();
                    break;
                case TileTypes.BlankTile:
                    tileTemplate = new BlankTile();
                    break;
                case TileTypes.MidTile:
                    tileTemplate = new MidTile();
                    break;
                default:
                    tileTemplate = null;
                    break;
            }

            return tileTemplate;
        }

        private bool IsCellDefined(Cell? cell)
        {
            if (cell == null || cell?.Tile == TileTypes.NotDefined)
            {
                return false;
            }

            return true;
        }

        private Cell? GetCell(int x, int y)
        {
            if ((x < 0 || x >= Width) || (y < 0 || y >= Height))
            {
                return null;
            }

            return _board[y, x];
        }

        private void GenerateBlankBoard()
        {
            for (int i = 0; i <= Height - 1; i++)
            {
                for(int j = 0; j <= Width - 1; j++)
                {
                    _board[i, j] = new Cell(j, i);
                }
            }

            Debug.WriteLine("- Default board created");
        }
    }
}
