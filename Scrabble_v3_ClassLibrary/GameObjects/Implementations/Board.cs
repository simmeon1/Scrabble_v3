using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabble_v3_ClassLibrary.GameObjects.Implementations
{
    public class Board
    {
        private BoardTileDto[][] Tiles { get; set; }
        public Board(IBoardTileArrayCreator tileArrayCreator, IEnumerable<BoardTileDto> tiles)
        {
            Tiles = tileArrayCreator.GetBoardTileArray(tiles);
        }

        public override string ToString()
        {
            StringBuilder sb = new("");
            foreach (BoardTileDto[] tileRow in Tiles)
            {
                if (sb.Length > 0) sb.Append(Environment.NewLine);
                foreach (BoardTileDto tile in tileRow)
                {
                    string letter = " ";
                    if (tile == null) letter = "~";
                    else if (!tile.Letter.Equals("")) letter = tile.Letter;
                    sb.Append($"[{letter}]");
                }
            }
            return sb.ToString();
        }
    }
}
