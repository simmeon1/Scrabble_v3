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
        Mock<IBoardTileArrayCreator> organiser;

        [TestInitialize]
        public void TestInitialize()
        {
            organiser = new();
        }

        [TestMethod]
        public void ToStringPrintsTwoHorizontalTilesWithLettersCorrectly()
        {

            Board board = GetBoard(organiser, new BoardTileDto[][] {
                new BoardTileDto[] { new BoardTileDto { Letter = "A" }, new BoardTileDto { Letter = "B" } }
            });
            Assert.IsTrue(board.ToString().Equals("[A][B]"));
        }

        [TestMethod]
        public void ToStringPrintsThreeHorizontalTilesWithLettersCorrectly()
        {
            Board board = GetBoard(organiser, new BoardTileDto[][] {
                new BoardTileDto[] { new BoardTileDto { Letter = "" }, new BoardTileDto { Letter = "B" }, new BoardTileDto { Letter = "C" } }
            });
            Assert.IsTrue(board.ToString().Equals("[ ][B][C]"));
        }
        
        [TestMethod]
        public void ToStringPrintsTwoVerticalTilesWithLettersCorrectly()
        {
            Board board = GetBoard(organiser, new BoardTileDto[][] {
                new BoardTileDto[] { new BoardTileDto { Letter = "A" } },
                new BoardTileDto[] { new BoardTileDto { Letter = "B" } }
            });
            Assert.IsTrue(board.ToString().Equals($"[A]{Environment.NewLine}[B]"));
        }

        [TestMethod]
        public void ToStringPrintsNullTilesCorrectly()
        {
            Board board = GetBoard(organiser, new BoardTileDto[][] {
                new BoardTileDto[] { null, null },
                new BoardTileDto[] { new BoardTileDto { Letter = "A" }, new BoardTileDto { Letter = "B" } }
            });
            Assert.IsTrue(board.ToString().Equals($"[~][~]{Environment.NewLine}[A][B]"));
        }

        private static Board GetBoard(Mock<IBoardTileArrayCreator> organiser, BoardTileDto[][] arr)
        {
            organiser.Setup(x => x.GetBoardTileArray(It.IsAny<IEnumerable<BoardTileDto>>())).Returns(arr);
            return new(organiser.Object, new List<BoardTileDto>());
        }
    }
}
