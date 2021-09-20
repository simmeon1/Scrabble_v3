using Scrabble_v3_ClassLibrary.DataObjects;

namespace Scrabble_v3_Tests.UnitTests.Helpers
{
    public static class BoardTileDtoCreator
    {
        public static BoardTileDto CreateTile()
        {
            return new(1, 1, 1, 1, true, "", 1);
        }
        
        public static BoardTileDto CreateTile(int row, int column, bool isStart = false, int id = 1, string letter = "", int score = 0)
        {
            return new(id, 1, row, column, isStart, letter, score);
        }

        public static BoardTileDto CreateTile(int id, int boardId, int row, int column, bool isStart, string letter, int score)
        {
            return new(id, boardId, row, column, isStart, letter, score);
        }

        public static BoardTileDto CreateTileWithLetter(string letter)
        {
            return new BoardTileDto(1, 1, 1, 1, true, letter, 0);
        }

        public static BoardTileDto CreateTileWithId(int id)
        {
            return new BoardTileDto(id, 1, 1, 1, true, "", 0);
        }
    }
}
