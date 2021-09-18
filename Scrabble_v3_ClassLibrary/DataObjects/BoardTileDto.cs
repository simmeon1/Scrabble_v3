using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble_v3_ClassLibrary.DataObjects
{
    public class BoardTileDto
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsStart { get; set; }
        public string Letter { get; set; }
        public int Score { get; set; }

        public BoardTileDto()
        {
        }

        public BoardTileDto(int id, int boardId, int row, int column, bool isStart, string letter, int score)
        {
            Id = id;
            BoardId = boardId;
            Row = row;
            Column = column;
            IsStart = isStart;
            Letter = letter;
            Score = score;
        }

        public override string ToString()
        {
            return $"Id = {Id}, BoardId = {BoardId}, Row = {Row}, Column = {Column}, IsStart = {IsStart}, Letter = {Letter}, Score = {Score}";
        }
    }
}
