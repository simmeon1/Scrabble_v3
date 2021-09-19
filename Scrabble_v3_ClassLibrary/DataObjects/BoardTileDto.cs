using System;

namespace Scrabble_v3_ClassLibrary.DataObjects
{
    public class BoardTileDto
    {
        public const string ID_BELOW_ONE = "Tile has an id below 1.";
        public const string BOARD_ID_BELOW_ONE = "Tile has board id below 1.";
        public const string ROW_BELOW_ZERO = "Tile has a row index below 1.";
        public const string COLUMN_BELOW_ZERO = "Tile has a column index below 1.";
        public const string LETTER_IS_NULL = "Letter is null.";
        public const string LETTER_MORE_THAN_ONE_CHARACTERS = "Letter is more than 1 characters.";
        public const string SCORE_BELOW_ZERO = "Tile has a score 0.";

        public int Id { get; set; }
        public int BoardId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsStart { get; set; }
        public string Letter { get; set; }
        public int Score { get; set; }

        public BoardTileDto(int id, int boardId, int row, int column, bool isStart, string letter, int score)
        {
            if (id < 1) throw new Exception(ID_BELOW_ONE);
            if (boardId < 1) throw new Exception(BOARD_ID_BELOW_ONE);
            if (row < 1) throw new Exception(ROW_BELOW_ZERO);
            if (column < 1) throw new Exception(COLUMN_BELOW_ZERO);
            if (letter == null) throw new Exception(LETTER_IS_NULL);
            if (letter.Length > 1) throw new Exception(LETTER_MORE_THAN_ONE_CHARACTERS);
            if (score < 0) throw new Exception(SCORE_BELOW_ZERO);

            Id = id;
            BoardId = boardId;
            Row = row;
            Column = column;
            IsStart = isStart;
            Letter = letter.Trim().ToUpper();
            Score = score;
        }

        public override string ToString()
        {
            return $"Id = {Id}, BoardId = {BoardId}, Row = {Row}, Column = {Column}, IsStart = {IsStart}, Letter = {Letter}, Score = {Score}";
        }
    }
}
