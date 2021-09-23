using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabble_v3_ClassLibrary.GameObjects.Implementations
{
    public class Board
    {
        public const string LETTER_IS_NOT_VALID = "Letter is not valid.";
        private BoardTileDto[][] Tiles { get; set; }
        private ILetterRepository LetterRepository { get; set; }
        public Board(IBoardTileArrayCreator tileArrayCreator, IEnumerable<BoardTileDto> tiles, ILetterRepository letterRepository)
        {
            Tiles = tileArrayCreator.GetBoardTileArray(tiles);
            LetterRepository = letterRepository;
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

        public void PlaceLetter(int row, int column, string letter, int score)
        {
            BoardTileDto tile = GetTile(row, column);
            Validators.ValidateLetter(letter);
            Validators.ValidateScore(score);
            if (!LetterRepository.LetterIsValid(letter)) throw new Exception(LETTER_IS_NOT_VALID);
            tile.Letter = letter;
            tile.Score = score;
        }

        public BoardWord GetHorizontalWordAtTile(int row, int column)
        {
            List<BoardTileDto> tiles = new();
            BoardTileDto tile = GetTile(row, column);
            if (tile.Letter.Equals("")) return new BoardWord(tiles);

            tiles.Add(tile);

            int columnTemp = column - 1;
            while (TileIsInPlayAndHasLetter(row, columnTemp) != null)
            {
                tiles.Insert(0, GetTile(row, columnTemp));
                columnTemp--;
            }

            columnTemp = column + 1;
            while (TileIsInPlayAndHasLetter(row, columnTemp) != null)
            {
                tiles.Add(GetTile(row, columnTemp));
                columnTemp++;
            }
            return new BoardWord(tiles);
        }
        
        public BoardWord GetVerticalWordAtTile(int row, int column)
        {
            List<BoardTileDto> tiles = new();
            BoardTileDto tile = GetTile(row, column);
            if (tile.Letter.Equals("")) return new BoardWord(tiles);

            tiles.Add(tile);

            int rowTemp = row - 1;
            while (TileIsInPlayAndHasLetter(rowTemp, column) != null)
            {
                tiles.Insert(0, GetTile(rowTemp, column));
                rowTemp--;
            }

            rowTemp = row + 1;
            while (TileIsInPlayAndHasLetter(rowTemp, column) != null)
            {
                tiles.Add(GetTile(rowTemp, column));
                rowTemp++;
            }
            return new BoardWord(tiles);
        }

        private BoardTileDto TileIsInPlayAndHasLetter(int row, int columnTemp)
        {
            BoardTileDto tile = GetTile(row, columnTemp, returnNullOnException: true);
            return tile != null && !tile.Letter.Equals("") ? tile : null;
        }

        public BoardTileDto GetTile(int row, int column, bool returnNullOnException = false)
        {
            int actualRowIndex = row - 1;
            int actualColumnIndex = column - 1;
            if (actualRowIndex < 0 || actualRowIndex >= Tiles.Length) return returnNullOnException ? null : throw new Exception($"Row {row} is not valid.");
            if (actualColumnIndex < 0 || actualColumnIndex >= Tiles[0].Length) return returnNullOnException ? null : throw new Exception($"Column {column} is not valid.");
            BoardTileDto tile = Tiles[actualRowIndex][actualColumnIndex];
            return tile ?? (returnNullOnException ? null : throw new Exception($"Tile at row {row}, column {column} is not in play."));
        }
    }
}
