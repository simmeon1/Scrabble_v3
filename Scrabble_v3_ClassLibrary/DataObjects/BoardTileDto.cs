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

        public BoardTileDto(int id, int boardId, int row, int column, bool isStart)
        {
            Id = id;
            BoardId = boardId;
            Row = row;
            Column = column;
            IsStart = isStart;
        }

        public override string ToString()
        {
            return $"Id = {Id}, Row = {Row}, Column = {Column}, IsStart = {IsStart}";
        }
    }
}
