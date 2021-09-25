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
                    else if (tile.HasLetter()) letter = tile.Letter;
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
            Direction direction = Direction.Horizontal;
            List<BoardTileDto> tiles = new();
            BoardTileDto tile = GetTile(row, column);
            if (tile.DoesNotHaveLetter()) return new BoardWord(direction, tiles);

            tiles.Add(tile);

            int columnTemp = column - 1;
            while (TileIsInPlayAndHasLetter(row, columnTemp))
            {
                tiles.Insert(0, GetTile(row, columnTemp));
                columnTemp--;
            }

            columnTemp = column + 1;
            while (TileIsInPlayAndHasLetter(row, columnTemp))
            {
                tiles.Add(GetTile(row, columnTemp));
                columnTemp++;
            }
            return new BoardWord(direction, tiles);
        }

        public BoardWord GetVerticalWordAtTile(int row, int column)
        {
            Direction direction = Direction.Vertical;
            List<BoardTileDto> tiles = new();
            BoardTileDto tile = GetTile(row, column);
            if (tile.DoesNotHaveLetter()) return new BoardWord(direction, tiles);

            tiles.Add(tile);

            int rowTemp = row - 1;
            while (TileIsInPlayAndHasLetter(rowTemp, column))
            {
                tiles.Insert(0, GetTile(rowTemp, column));
                rowTemp--;
            }

            rowTemp = row + 1;
            while (TileIsInPlayAndHasLetter(rowTemp, column))
            {
                tiles.Add(GetTile(rowTemp, column));
                rowTemp++;
            }
            return new BoardWord(direction, tiles);
        }

        private bool TileIsInPlayAndHasLetter(int row, int column)
        {
            BoardTileDto tile = GetTile(row, column, returnNullInsteadOfException: true);
            return tile != null && tile.HasLetter();
        }

        public List<BoardTileDto> GetAnchors()
        {
            List<BoardTileDto> anchors = new();
            foreach (BoardTileDto[] rowOfTiles in Tiles)
            {
                foreach (BoardTileDto tile in rowOfTiles)
                {
                    if (tile == null || tile.HasLetter()) continue;
                    if (TileIsInPlayAndHasLetter(tile.Row + 1, tile.Column) ||
                        TileIsInPlayAndHasLetter(tile.Row - 1, tile.Column) ||
                        TileIsInPlayAndHasLetter(tile.Row, tile.Column + 1) ||
                        TileIsInPlayAndHasLetter(tile.Row, tile.Column - 1)) anchors.Add(tile);
                }
            }
            return anchors;
        }

        public BoardTileDto GetTile(int row, int column, bool returnNullInsteadOfException = false)
        {
            int actualRowIndex = row - 1;
            int actualColumnIndex = column - 1;
            if (actualRowIndex < 0 || actualRowIndex >= Tiles.Length) return returnNullInsteadOfException ? null : throw new Exception($"Row {row} is not valid.");
            if (actualColumnIndex < 0 || actualColumnIndex >= Tiles[0].Length) return returnNullInsteadOfException ? null : throw new Exception($"Column {column} is not valid.");
            BoardTileDto tile = Tiles[actualRowIndex][actualColumnIndex];
            return tile ?? (returnNullInsteadOfException ? null : throw new Exception($"Tile at row {row}, column {column} is not in play."));
        }
    }
}
