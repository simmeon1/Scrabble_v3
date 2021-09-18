using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects.Implementations;
using Scrabble_v3_ClassLibrary.GameObjects.Interfaces;
using System;
using System.Collections.Generic;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class Board_UnitTests
    {
        Mock<IBoardTileArrayCreator> tileArrayCreator;

        [TestInitialize]
        public void TestInitialize()
        {
            tileArrayCreator = new();
        }

        [TestMethod]
        public void ToStringPrintsTwoHorizontalTilesWithLettersCorrectly()
        {

            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] {
                new BoardTileDto[] { new BoardTileDto { Letter = "A" }, new BoardTileDto { Letter = "B" } }
            });
            Assert.IsTrue(board.ToString().Equals("[A][B]"));
        }

        [TestMethod]
        public void ToStringPrintsThreeHorizontalTilesWithLettersCorrectly()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] {
                new BoardTileDto[] { new BoardTileDto { Letter = "" }, new BoardTileDto { Letter = "B" }, new BoardTileDto { Letter = "C" } }
            });
            Assert.IsTrue(board.ToString().Equals("[ ][B][C]"));
        }

        [TestMethod]
        public void ToStringPrintsTwoVerticalTilesWithLettersCorrectly()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] {
                new BoardTileDto[] { new BoardTileDto { Letter = "A" } },
                new BoardTileDto[] { new BoardTileDto { Letter = "B" } }
            });
            Assert.IsTrue(board.ToString().Equals($"[A]{Environment.NewLine}[B]"));
        }

        [TestMethod]
        public void ToStringPrintsNullTilesCorrectly()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] {
                new BoardTileDto[] { null, null },
                new BoardTileDto[] { new BoardTileDto { Letter = "A" }, new BoardTileDto { Letter = "B" } }
            });
            Assert.IsTrue(board.ToString().Equals($"[~][~]{Environment.NewLine}[A][B]"));
        }
        
        [TestMethod]
        public void GetTileGetsCorrectTileFromBoardWithMultipleRows()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { new BoardTileDto { Id = 1 } }, new BoardTileDto[] { new BoardTileDto { Id = 2 } } });
            Assert.IsTrue(board.GetTile(2, 1).Id == 2);
        }
        
        [TestMethod]
        public void GetTileGetsCorrectTileFromBoardWithMultipleColumns()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { new BoardTileDto { Id = 1 }, new BoardTileDto { Id = 2 } } });
            Assert.IsTrue(board.GetTile(1, 2).Id == 2);
        }
        
        [TestMethod]
        public void GetTileThrowsExceptionForNullTile()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { null } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(1, 1), "Tile at row 1, column 1 is not in play.");
        }
        
        [TestMethod]
        public void GetTileThrowsExceptionForRowBelowZero()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { new BoardTileDto() } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(-1, 1), "Row -1 is not valid.");
        }
        
        [TestMethod]
        public void GetTileThrowsExceptionForRowAboveTheLast()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { new BoardTileDto() }, new BoardTileDto[] { new BoardTileDto() } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(3, 1), "Row 3 is not valid.");
        }
        
        [TestMethod]
        public void GetTileThrowsExceptionForColumnBelowZero()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { new BoardTileDto() } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(1, -1), $"Column -1 is not valid.");
        }
        
        [TestMethod]
        public void GetTileThrowsExceptionForColumnAboveTheLast()
        {
            Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { new BoardTileDto(), new BoardTileDto() } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(1, 3), $"Column 3 is not valid.");
        }

        //[TestMethod]
        //public void PlaceLetterPlacesLetterCorrectly()
        //{
        //    Board board = GetBoard(tileArrayCreator, new BoardTileDto[][] { new BoardTileDto[] { new BoardTileDto { Letter = "" } } });
        //    board.PlaceLetter(1, 1, "A", 5);
        //    Assert.IsTrue(board.GetTile(1,1));
        //}

        private static Board GetBoard(Mock<IBoardTileArrayCreator> tileArrayCreator, BoardTileDto[][] arr)
        {
            tileArrayCreator.Setup(x => x.GetBoardTileArray(It.IsAny<IEnumerable<BoardTileDto>>())).Returns(arr);
            return new(tileArrayCreator.Object, new List<BoardTileDto>());
        }
    }
}
