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
        readonly BoardTileArrayCreator tileArrayCreator = new();

        [TestMethod]
        public void TwoVerticallyConnectedTilesSuccessfullyCreated()
        {
            BoardTileDto[][] tiles = tileArrayCreator.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(2, 2, true, 1), BoardTileDtoCreator.CreateTile(2, 3, false, 2) });
            Assert.IsTrue(tiles.Length == 2);
            Assert.IsTrue(tiles[0].Length == 3);
            Assert.IsTrue(tiles[1][1].Id == 1);
            Assert.IsTrue(tiles[1][2].Id == 2);
        }
        
        [TestMethod]
        public void TwoHorizontallyConnectedTilesSuccessfullyCreated()
        {
            BoardTileDto[][] tiles = tileArrayCreator.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(1, 2, true, 1), BoardTileDtoCreator.CreateTile(2, 2, false, 2) });
            Assert.IsTrue(tiles.Length == 2);
            Assert.IsTrue(tiles[0].Length == 2);
            Assert.IsTrue(tiles[0][1].Id == 1);
            Assert.IsTrue(tiles[1][1].Id == 2);
        }
        
        [TestMethod]
        public void ArrayWithNullTileThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => tileArrayCreator.GetBoardTileArray(new List<BoardTileDto>() { null }), BoardTileArrayCreator.TILE_IS_NULL);
        }
        
        [TestMethod]
        public void ArrayWithNoStartTileThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => tileArrayCreator.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(1, 1) }), BoardTileArrayCreator.COUNT_OF_START_TILES_MUST_BE_EXACTLY_1);
        }

        [TestMethod]
        public void SingleTileThrowsExceptionForNoConnections()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(3, 3, true);
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => tileArrayCreator.GetBoardTileArray(new List<BoardTileDto>() { tile }), $"Tile {tile} is not horizontally or vertically connected to another tile.");
        }
        
        [TestMethod]
        public void TwoTilesThrowsExceptionForNoConnections()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(1, 1, true);
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(
                () => tileArrayCreator.GetBoardTileArray(new List<BoardTileDto>() { tile, BoardTileDtoCreator.CreateTile(3, 3) }),
                $"Tile {tile} is not horizontally or vertically connected to another tile.");
        }
        
        [TestMethod]
        public void TileAlreadyPlaced()
        {
            BoardTileDto tile = BoardTileDtoCreator.CreateTile(1, 1, false, 2);
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => tileArrayCreator.GetBoardTileArray(new List<BoardTileDto>() { BoardTileDtoCreator.CreateTile(1, 1, true, 1), tile }), $"Tile {tile} is already initialized.");
        }
    }
}
