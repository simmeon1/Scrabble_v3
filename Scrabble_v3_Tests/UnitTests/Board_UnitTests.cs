using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects.Implementations;
using Scrabble_v3_Tests.UnitTests.Helpers;
using System;
using System.Collections.Generic;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class Board_UnitTests
    {
        readonly BoardTileArrayCreator organiser = new();

        [TestMethod]
        public void ToStringPrintsTwoHorizontalTilesWithLettersCorrectly()
        {
            Board board = CreateBoardFromTiles(new List<BoardTileDto>() {
                BoardTileDtoCreator.CreateTile(1, 1, true, letter: "A"),
                BoardTileDtoCreator.CreateTile(1, 2, false, letter: "B")
            });
            Assert.IsTrue(board.ToString().Equals("[A][B]"));
        }

        [TestMethod]
        public void ToStringPrintsThreeHorizontalTilesWithLettersCorrectly()
        {
            Board board = CreateBoardFromTiles(new List<BoardTileDto>() {
                BoardTileDtoCreator.CreateTile(1, 1, true, letter: ""),
                BoardTileDtoCreator.CreateTile(1, 2, false, letter: "B"),
                BoardTileDtoCreator.CreateTile(1, 3, false, letter: "C")
            });
            Assert.IsTrue(board.ToString().Equals("[ ][B][C]"));
        }
        
        [TestMethod]
        public void ToStringPrintsTwoVerticalTilesWithLettersCorrectly()
        {
            Board board = CreateBoardFromTiles(new List<BoardTileDto>() {
                BoardTileDtoCreator.CreateTile(1, 1, true, letter: "A"),
                BoardTileDtoCreator.CreateTile(2, 1, false, letter: "B")
            });
            Assert.IsTrue(board.ToString().Equals($"[A]{Environment.NewLine}[B]"));
        }

        [TestMethod]
        public void ToStringPrintsNullTilesCorrectly()
        {
            Board board = CreateBoardFromTiles(new List<BoardTileDto>() {
                BoardTileDtoCreator.CreateTile(2, 1, true, letter: "A"),
                BoardTileDtoCreator.CreateTile(2, 2, false, letter: "B")
            });
            Assert.IsTrue(board.ToString().Equals($"[~][~]{Environment.NewLine}[A][B]"));
        }

        private Board CreateBoardFromTiles(List<BoardTileDto> tiles)
        {
            return new(organiser.GetBoardTileArray(tiles));
        }
    }
}
