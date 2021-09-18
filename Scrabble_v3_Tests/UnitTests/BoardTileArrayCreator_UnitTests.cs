using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects.Implementations;
using Scrabble_v3_Tests.UnitTests.Helpers;
using System;
using System.Collections.Generic;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class BoardTileArrayCreator_UnitTests
    {
        readonly BoardTileArrayCreator organiser = new();

        [TestMethod]
        public void TwoVerticallyConnectedTilesSuccessfullyCreated()
        {
            BoardTileDto[][] tiles = organiser.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(2, 2, true, 1), BoardTileDtoCreator.CreateTile(2, 3, false, 2) });
            Assert.IsTrue(tiles.Length == 2);
            Assert.IsTrue(tiles[0].Length == 3);
            Assert.IsTrue(tiles[1][1].Id == 1);
            Assert.IsTrue(tiles[1][2].Id == 2);
        }
        
        [TestMethod]
        public void TwoHorizontallyConnectedTilesSuccessfullyCreated()
        {
            BoardTileDto[][] tiles = organiser.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(1, 2, true, 1), BoardTileDtoCreator.CreateTile(2, 2, false, 2) });
            Assert.IsTrue(tiles.Length == 2);
            Assert.IsTrue(tiles[0].Length == 2);
            Assert.IsTrue(tiles[0][1].Id == 1);
            Assert.IsTrue(tiles[1][1].Id == 2);
        }
        
        [TestMethod]
        public void ArrayWithNullTileThrowsException()
        {
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { null }), BoardTileArrayCreator.TILE_IS_NULL);
        }
        
        [TestMethod]
        public void ArrayWithNoRowsThrowsException()
        {
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(0, 1, true) }), BoardTileArrayCreator.ROWS_MUST_BE_MORE_THAN_0);
        }

        [TestMethod]
        public void ArrayWithNoColumnsThrowsException()
        {
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(1, 0, true) }), BoardTileArrayCreator.COLUMNS_MUST_BE_MORE_THAN_0);
        }
        
        [TestMethod]
        public void ArrayWithNoStartTileThrowsException()
        {
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(1, 1) }), BoardTileArrayCreator.COUNT_OF_START_TILES_MUST_BE_EXACTLY_1);
        }
        
        [TestMethod]
        public void TileWithRowBelowZeroThrowsException()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(-1, 0);
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { tile }), $"Tile {tile} has a row index below 0.");
        }
        
        [TestMethod]
        public void TileWithColumnBelowZeroThrowsException()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(1, -1);
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { tile }), $"Tile {tile} has a column index below 0.");
        }

        [TestMethod]
        public void SingleTileThrowsExceptionForNoConnections()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(3, 3, true);
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { tile }), $"Tile {tile} is not horizontally or vertically connected to another tile.");
        }
        
        [TestMethod]
        public void TwoTilesThrowsExceptionForNoConnections()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(1, 1, true);
            AssertExceptionWithMessageIsThrown(
                () => organiser.GetBoardTileArray(new List<BoardTileDto>() { tile, BoardTileDtoCreator.CreateTile(3, 3) }),
                $"Tile {tile} is not horizontally or vertically connected to another tile.");
        }
        
        [TestMethod]
        public void TileAlreadyPlaced()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(1, 1, false, 2);
            AssertExceptionWithMessageIsThrown(() => organiser.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(1, 1, true, 1), tile }), $"Tile {tile} is already initialized.");
        }

        private static void AssertExceptionWithMessageIsThrown(Func<BoardTileDto[][]> getTilesFunc, string message)
        {
            try
            {
                getTilesFunc.Invoke();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals(message), $"Exception message is '{ex.Message}', expected '{message}'.");
            }
        }
    }
}
