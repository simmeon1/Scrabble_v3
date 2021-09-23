using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Scrabble_v3_ClassLibrary;
using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_ClassLibrary.GameObjects.Implementations;
using Scrabble_v3_ClassLibrary.GameObjects.Interfaces;
using Scrabble_v3_Tests.UnitTests.Helpers;
using System;
using System.Collections.Generic;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class Board_UnitTests
    {
        Mock<IBoardTileArrayCreator> tileArrayCreator;
        Mock<ILetterRepository> letterRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            tileArrayCreator = new();
            letterRepo = new();
        }

        [TestMethod]
        public void ToStringPrintsTwoHorizontalTilesWithLettersCorrectly()
        {

            Board board = GetBoardWithTiles(new BoardTileDto[][] {
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("A"), BoardTileDtoCreator.CreateTileWithLetter("B") }
            });
            Assert.IsTrue(board.ToString().Equals("[A][B]"));
        }

        [TestMethod]
        public void ToStringPrintsThreeHorizontalTilesWithLettersCorrectly()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] {
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter(""), BoardTileDtoCreator.CreateTileWithLetter("B"), BoardTileDtoCreator.CreateTileWithLetter("C") }
            });
            Assert.IsTrue(board.ToString().Equals("[ ][B][C]"));
        }

        [TestMethod]
        public void ToStringPrintsTwoVerticalTilesWithLettersCorrectly()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] {
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("A") },
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("B") }
            });
            Assert.IsTrue(board.ToString().Equals($"[A]{Environment.NewLine}[B]"));
        }

        [TestMethod]
        public void ToStringPrintsNullTilesCorrectly()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] {
                new BoardTileDto[] { null, null },
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("A"), BoardTileDtoCreator.CreateTileWithLetter("B") }
            });
            Assert.IsTrue(board.ToString().Equals($"[~][~]{Environment.NewLine}[A][B]"));
        }

        [TestMethod]
        public void GetTileGetsCorrectTileFromBoardWithMultipleRows()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithId(1) }, new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithId(2) } });
            Assert.IsTrue(board.GetTile(2, 1).Id == 2);
        }

        [TestMethod]
        public void GetTileGetsCorrectTileFromBoardWithMultipleColumns()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithId(1), BoardTileDtoCreator.CreateTileWithId(2) } });
            Assert.IsTrue(board.GetTile(1, 2).Id == 2);
        }

        [TestMethod]
        public void GetTileThrowsExceptionForNullTile()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { null } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(1, 1), "Tile at row 1, column 1 is not in play.");
        }

        [TestMethod]
        public void GetTileThrowsExceptionForRowBelowZero()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().GetTile(-1, 1), "Row -1 is not valid.");
        }

        [TestMethod]
        public void GetTileThrowsExceptionForRowAboveTheLast()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTile() }, new BoardTileDto[] { BoardTileDtoCreator.CreateTile() } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(3, 1), "Row 3 is not valid.");
        }

        [TestMethod]
        public void GetTileThrowsExceptionForColumnBelowZero()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().GetTile(1, -1), $"Column -1 is not valid.");
        }

        [TestMethod]
        public void GetTileThrowsExceptionForColumnAboveTheLast()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTile(), BoardTileDtoCreator.CreateTile() } });
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.GetTile(1, 3), $"Column 3 is not valid.");
        }

        [TestMethod]
        public void GetTileGetsCorrectTileFromBoardWithMultipleRows_ReturnNullOnException()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithId(1) }, new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithId(2) } });
            Assert.IsTrue(board.GetTile(2, 1, returnNullOnException: true).Id == 2);
        }

        [TestMethod]
        public void GetTileGetsCorrectTileFromBoardWithMultipleColumns_ReturnNullOnException()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithId(1), BoardTileDtoCreator.CreateTileWithId(2) } });
            Assert.IsTrue(board.GetTile(1, 2, returnNullOnException: true).Id == 2);
        }

        [TestMethod]
        public void GetTileReturnsNullForNullTile()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { null } });
            Assert.IsTrue(board.GetTile(1, 1, returnNullOnException: true) == null);
        }

        [TestMethod]
        public void GetTileReturnsNullForRowBelowZero()
        {
            Assert.IsTrue(GetBoardWithOneTile().GetTile(-1, 1, returnNullOnException: true) == null);
        }

        [TestMethod]
        public void GetTileReturnsNullForRowAboveTheLast()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTile() }, new BoardTileDto[] { BoardTileDtoCreator.CreateTile() } });
            Assert.IsTrue(board.GetTile(3, 1, returnNullOnException: true) == null);
        }

        [TestMethod]
        public void GetTileReturnsNullForColumnBelowZero()
        {
            Assert.IsTrue(GetBoardWithOneTile().GetTile(1, -1, returnNullOnException: true) == null);
        }

        [TestMethod]
        public void GetTileReturnsNullForColumnAboveTheLast()
        {
            Board board = GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTile(), BoardTileDtoCreator.CreateTile() } });
            Assert.IsTrue(board.GetTile(1, 3, returnNullOnException: true) == null);
        }

        [TestMethod]
        public void PlaceLetterPlacesLetterCorrectly()
        {
            letterRepo.Setup(x => x.LetterIsValid(It.IsAny<string>())).Returns(true);
            Board board = GetBoardWithOneTile();
            board.PlaceLetter(1, 1, "C", 5);
            BoardTileDto boardTileDto = board.GetTile(1, 1);
            Assert.IsTrue(boardTileDto.Letter.Equals("C"));
            Assert.IsTrue(boardTileDto.Score == 5);
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToInvalidCharacter()
        {
            letterRepo.Setup(x => x.LetterIsValid(It.IsAny<string>())).Returns(false);
            Board board = GetBoardWithOneTile();
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => board.PlaceLetter(1, 1, "C", 5), Board.LETTER_IS_NOT_VALID);
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToNullLetter()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().PlaceLetter(1, 1, null, 5), Validators.LETTER_IS_NULL);
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToMultipleCharacters()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().PlaceLetter(1, 1, "ab", 5), Validators.LETTER_MORE_THAN_ONE_CHARACTERS);
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToNegativeScore()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().PlaceLetter(1, 1, "A", -1), Validators.SCORE_BELOW_ZERO);
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToNegativeRow()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().PlaceLetter(-1, 1, "A", 1), "Row -1 is not valid.");
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToRowTooHigh()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().PlaceLetter(2, 1, "A", 1), "Row 2 is not valid.");
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToNegativeColumn()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().PlaceLetter(1, -1, "A", 1), "Column -1 is not valid.");
        }

        [TestMethod]
        public void PlaceLetterThrowsExceptionDueToColumnTooHigh()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardWithOneTile().PlaceLetter(1, 2, "A", 1), "Column 2 is not valid.");
        }

        [TestMethod]
        public void GetHorizontalWordAtTileOneOneReturnsEmptyString()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(1, 1).ToString().Equals(""));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileOneTwoReturnsAB()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(1, 2).ToString().Equals("AB"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileOneThreeReturnsAB()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(1, 3).ToString().Equals("AB"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileTwoOneReturnsCDE()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(2, 1).ToString().Equals("CDE"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileTwoTwoReturnsCDE()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(2, 2).ToString().Equals("CDE"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileTwoThreeReturnsCDE()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(2, 3).ToString().Equals("CDE"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileThreeOneReturnsFG()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(3, 1).ToString().Equals("FG"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileThreeTwoReturnsFG()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(3, 2).ToString().Equals("FG"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileThreeThreeThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardForGetWordTests().GetHorizontalWordAtTile(3, 3), "Tile at row 3, column 3 is not in play.");
        }

        [TestMethod]
        public void GetHorizontalWordAtTileTenTenThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardForGetWordTests().GetHorizontalWordAtTile(10, 10), "Row 10 is not valid.");
        }

        [TestMethod]
        public void GetHorizontalWordAtTileFourOneReturnsH()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(4, 1).ToString().Equals("H"));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileFourTwoReturnsEmpty()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(4, 2).ToString().Equals(""));
        }

        [TestMethod]
        public void GetHorizontalWordAtTileFourThreeReturnsI()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetHorizontalWordAtTile(4, 3).ToString().Equals("I"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileOneOneReturnsEmptyString()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(1, 1).ToString().Equals(""));
        }

        [TestMethod]
        public void GetVerticalWordAtTileOneTwoReturnsADG()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(1, 2).ToString().Equals("ADG"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileOneThreeReturnsBE()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(1, 3).ToString().Equals("BE"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileTwoOneReturnsCFHJ()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(2, 1).ToString().Equals("CFHJ"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileTwoTwoReturnsADG()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(2, 2).ToString().Equals("ADG"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileTwoThreeReturnsBE()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(2, 3).ToString().Equals("BE"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileThreeOneReturnsCFHJ()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(3, 1).ToString().Equals("CFHJ"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileThreeTwoReturnsADG()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(3, 2).ToString().Equals("ADG"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileThreeThreeThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardForGetWordTests().GetVerticalWordAtTile(3, 3), "Tile at row 3, column 3 is not in play.");
        }

        [TestMethod]
        public void GetVerticalWordAtTileTenTenThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => GetBoardForGetWordTests().GetVerticalWordAtTile(10, 10), "Row 10 is not valid.");
        }

        [TestMethod]
        public void GetVerticalWordAtTileFourOneReturnsCFHJ()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(4, 1).ToString().Equals("CFHJ"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileFourTwoReturnsEmpty()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(4, 2).ToString().Equals(""));
        }

        [TestMethod]
        public void GetVerticalWordAtTileFourThreeReturnsI()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(4, 3).ToString().Equals("I"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileFiveOneReturnsCFHJ()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(5, 1).ToString().Equals("CFHJ"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileFiveTwoReturnsK()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(5, 2).ToString().Equals("K"));
        }

        [TestMethod]
        public void GetVerticalWordAtTileFiveThreeReturnsI()
        {
            Assert.IsTrue(GetBoardForGetWordTests().GetVerticalWordAtTile(5, 3).ToString().Equals(""));
        }

        private Board GetBoardForGetWordTests()
        {
            return GetBoardWithTiles(new BoardTileDto[][] {
                new BoardTileDto[] { BoardTileDtoCreator.CreateTile(), BoardTileDtoCreator.CreateTileWithLetter("A"), BoardTileDtoCreator.CreateTileWithLetter("B") },
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("C"), BoardTileDtoCreator.CreateTileWithLetter("D"), BoardTileDtoCreator.CreateTileWithLetter("E") },
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("F"), BoardTileDtoCreator.CreateTileWithLetter("G"), null },
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("H"), BoardTileDtoCreator.CreateTile(), BoardTileDtoCreator.CreateTileWithLetter("I") },
                new BoardTileDto[] { BoardTileDtoCreator.CreateTileWithLetter("J"), BoardTileDtoCreator.CreateTileWithLetter("K"), BoardTileDtoCreator.CreateTile() },
            });
        }

        private Board GetBoardWithOneTile()
        {
            return GetBoardWithTiles(new BoardTileDto[][] { new BoardTileDto[] { BoardTileDtoCreator.CreateTile() } });
        }

        private Board GetBoardWithTiles(BoardTileDto[][] tiles)
        {
            tileArrayCreator.Setup(x => x.GetBoardTileArray(It.IsAny<IEnumerable<BoardTileDto>>())).Returns(tiles);
            return new(tileArrayCreator.Object, new List<BoardTileDto>(), letterRepo.Object);
        }
    }
}
