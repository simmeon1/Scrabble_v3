using Scrabble_v3_ClassLibrary.DataObjects;
using System.Collections.Generic;
using System.Text;

namespace Scrabble_v3_ClassLibrary.GameObjects.Implementations
{
    public class BoardWord
    {
        private List<BoardTileDto> Tiles { get; set; }
        public BoardWord(List<BoardTileDto> tiles)
        {
            Tiles = tiles;
        }

        public override string ToString()
        {
            StringBuilder sb = new("");
            foreach (BoardTileDto tile in Tiles)
            {
                sb.Append(tile.Letter);
            }
            return sb.ToString();
        }
    }
}