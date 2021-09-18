using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects;
using System;
using System.Collections.Generic;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class BoardTileOrganiser_UnitTests
    {
        BoardTileOrganiser organiser = new();

        [TestMethod]
        public void TwoVerticallyConnectedTilesSuccessfullyCreated()
        {
            BoardTileDto[][] tiles = organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(2, 2, 1), CreateTile(2, 3, 2) });
            Assert.IsTrue(tiles.Length == 2);
            Assert.IsTrue(tiles[0].Length == 3);
            Assert.IsTrue(tiles[1][1].Id == 1);
            Assert.IsTrue(tiles[1][2].Id == 2);
        }
        
        [TestMethod]
        public void TwoHorizontallyConnectedTilesSuccessfullyCreated()
        {
            BoardTileDto[][] tiles = organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(1, 2, 1), CreateTile(2, 2, 2) });
            Assert.IsTrue(tiles.Length == 2);
            Assert.IsTrue(tiles[0].Length == 2);
            Assert.IsTrue(tiles[0][1].Id == 1);
            Assert.IsTrue(tiles[1][1].Id == 2);
        }
        
        [TestMethod]
        public void ArrayWithNoRowsThrowsException()
        {
            Assert.ThrowsException<Exception>(() => organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(0, 1) }));
        }
        
        [TestMethod]
        public void ArrayWithNoColumnsThrowsException()
        {
            Assert.ThrowsException<Exception>(() => organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(1, 0) }));
        }
        
        [TestMethod]
        public void TileWithRowBelowZeroThrowsException()
        {
            Assert.ThrowsException<Exception>(() => organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(-1, 0) }));
        }
        
        [TestMethod]
        public void TileWithColumnBelowZeroThrowsException()
        {
            Assert.ThrowsException<Exception>(() => organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(1, -1) }));
        }

        [TestMethod]
        public void SingleTileThrowsExceptionForNoConnections()
        {
            Assert.ThrowsException<Exception>(() => organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(3, 3) }));
        }
        
        [TestMethod]
        public void TwoTilesThrowsExceptionForNoConnections()
        {
            Assert.ThrowsException<Exception>(() => organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(2, 2), CreateTile(3, 3) }));
        }
        
        [TestMethod]
        public void TileAlreadyPlaced()
        {
            Assert.ThrowsException<Exception>(() => organiser.GetOrganisedTiles(new List<BoardTileDto>() { CreateTile(1, 1, 1), CreateTile(1, 1, 2) }));
        }

        private static BoardTileDto CreateTile(int row, int column, int id = 1)
        {
            return new(id, 1, row, column);
        }
    }
}
