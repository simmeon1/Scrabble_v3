using Scrabble_v3_ClassLibrary.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble_v3_ClassLibrary.GameObjects
{
    public class BoardTileOrganiser
    {
        public const string ROWS_MUST_BE_MORE_THAN_0 = "Rows must be more than 0";
        public const string COLUMNS_MUST_BE_MORE_THAN_0 = "Columns must be more than 0";
        public const string COUNT_OF_START_TILES_MUST_BE_EXACTLY_1 = "Count of start tiles must be exactly 1.";

        public BoardTileDto[][] GetOrganisedTiles(IEnumerable<BoardTileDto> tiles)
        {
            int highestRow = 0;
            int highestColumn = 0;
            int countOfStartTiles = 0;

            foreach (BoardTileDto tile in tiles)
            {
                if (tile.Row < 0) throw new Exception($"Tile {tile} has a row index below 0.");
                if (tile.Column < 0) throw new Exception($"Tile {tile} has a column index below 0.");

                if (tile.IsStart) countOfStartTiles++;
                if (tile.Row > highestRow) highestRow = tile.Row;
                if (tile.Column > highestColumn) highestColumn = tile.Column;
            }

            if (countOfStartTiles != 1) throw new Exception(COUNT_OF_START_TILES_MUST_BE_EXACTLY_1);
            if (highestRow == 0) throw new Exception(ROWS_MUST_BE_MORE_THAN_0);
            if (highestColumn == 0) throw new Exception(COLUMNS_MUST_BE_MORE_THAN_0);

            BoardTileDto[][] organisedTiles = new BoardTileDto[highestRow][];
            for (int i = 0; i < organisedTiles.Length; i++) organisedTiles[i] = new BoardTileDto[highestColumn];

            foreach (BoardTileDto tile in tiles)
            {
                if (organisedTiles[tile.Row - 1][tile.Column - 1] != null) throw new Exception($"Tile {tile} is already initialized.");
                organisedTiles[tile.Row - 1][tile.Column - 1] = tile;
            }

            for (int i = 0; i < organisedTiles.Length; i++)
            {
                for (int j = 0; j < organisedTiles[i].Length; j++)
                {
                    BoardTileDto tile = organisedTiles[i][j];
                    if (tile == null) continue;
                    bool tileIsHorizontallyConnectedToTile = (j > 0 && organisedTiles[i][j - 1] != null) || (j < organisedTiles[i].Length - 1 && organisedTiles[i][j + 1] != null);
                    bool tileIsVerticallyConnectedToTile = (i > 0 && organisedTiles[i - 1][j] != null) || (i < organisedTiles.Length - 1 && organisedTiles[i + 1][j] != null);
                    if (!tileIsHorizontallyConnectedToTile && !tileIsVerticallyConnectedToTile) throw new Exception($"Tile {tile} is not horizontally or vertically connected to another tile.");
                }
            }
            return organisedTiles;
        }
    }
}
