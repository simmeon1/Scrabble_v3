using Scrabble_v3_ClassLibrary.DataObjects;

namespace Scrabble_v3_Tests.UnitTests.Helpers
{
    public static class BoardTileDtoCreator
    {
        public static BoardTileDto CreateTile(int row, int column, bool isStart = false, int id = 1, string letter = "", int score = 0)
        {
            return new(id, 1, row, column, isStart, letter, score);
        }
    }
}
