using Scrabble_v3_ClassLibrary.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabble_v3_ClassLibrary.GameObjects.Implementations
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public class BoardWord
    {
        private Direction Direction{ get; set; }
        private List<BoardTileDto> Tiles { get; set; }
        public BoardWord(Direction direction, List<BoardTileDto> tiles)
        {
            Direction = direction;
            Tiles = tiles;
            ValidateTiles();
        }

        private void ValidateTiles()
        {
            Func<BoardTileDto, int> constantCoordinate = (t) => t.Row;
            Func<BoardTileDto, int> incrementalCoordinate = (t) => t.Column;
            string constantKeyword = "Rows";
            string incrementalKeyword = "Columns";
            if (Direction == Direction.Vertical)
            {
                constantCoordinate = (t) => t.Column;
                incrementalCoordinate = (t) => t.Row;
                constantKeyword = "Columns";
                incrementalKeyword = "Rows";
            }

            int lastConstantCoordinate = 0;
            int lastIncrementalCoordinate = 0;
            foreach (BoardTileDto tile in Tiles)
            {
                int lastConstantCoordinateTemp = lastConstantCoordinate;
                lastConstantCoordinate = constantCoordinate.Invoke(tile);
                if (lastConstantCoordinateTemp != 0 && lastConstantCoordinate != lastConstantCoordinateTemp)
                {
                    throw new Exception($"{constantKeyword} in board word are not constant.");
                }
                
                int lastIncrementalCoordinateTemp = lastIncrementalCoordinate;
                lastIncrementalCoordinate = incrementalCoordinate.Invoke(tile);
                if (lastIncrementalCoordinateTemp == 0) continue;
                if (lastIncrementalCoordinate != lastIncrementalCoordinateTemp + 1)
                {
                    throw new Exception($"{incrementalKeyword} in board word are not incremental.");
                }
            }
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