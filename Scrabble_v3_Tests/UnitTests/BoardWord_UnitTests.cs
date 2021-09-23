using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble_v3_ClassLibrary;
using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects.Implementations;
using Scrabble_v3_Tests.UnitTests.Helpers;
using System.Collections.Generic;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class BoardWord_UnitTests
    {
        [TestMethod]
        public void ExceptionThrownForNotConstantRowsInHorizontalWord()
        {
            List<BoardTileDto> tiles = new() { BoardTileDtoCreator.CreateTileWithCoordinates(1, 1), BoardTileDtoCreator.CreateTileWithCoordinates(2, 1) };
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => CreateBoardWord(Direction.Horizontal, tiles), $"Rows in board word are not constant.");
        }
        
        [TestMethod]
        public void ExceptionThrownForNotConstantColumnsInVerticalWord()
        {
            List<BoardTileDto> tiles = new() { BoardTileDtoCreator.CreateTileWithCoordinates(1, 1), BoardTileDtoCreator.CreateTileWithCoordinates(1, 2) };
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => CreateBoardWord(Direction.Vertical, tiles), $"Columns in board word are not constant.");
        }
        
        [TestMethod]
        public void ExceptionThrownForNotIncrementingRowsInVerticalWord()
        {
            List<BoardTileDto> tiles = new() { BoardTileDtoCreator.CreateTileWithCoordinates(1, 1), BoardTileDtoCreator.CreateTileWithCoordinates(3, 1) };
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => CreateBoardWord(Direction.Vertical, tiles), $"Rows in board word are not incremental.");
        }
        
        [TestMethod]
        public void ExceptionThrownForNotIncrementingColumnsInHorizontalWord()
        {
            List<BoardTileDto> tiles = new() { BoardTileDtoCreator.CreateTileWithCoordinates(1, 1), BoardTileDtoCreator.CreateTileWithCoordinates(1, 3) };
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => CreateBoardWord(Direction.Horizontal, tiles), $"Columns in board word are not incremental.");
        }

        private static BoardWord CreateBoardWord(Direction direction, List<BoardTileDto> tiles)
        {
            return new BoardWord(direction, tiles);
        }
    }
}
