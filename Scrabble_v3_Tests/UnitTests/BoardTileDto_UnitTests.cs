using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble_v3_ClassLibrary.DataObjects;
using Scrabble_v3_Tests.UnitTests.Helpers;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class BoardTileDto_UnitTests
    {
        [TestMethod]
        public void IdBelowOneThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => new BoardTileDto(0, 1, 1, 1, true, "", 0), BoardTileDto.ID_BELOW_ONE);
        }
        
        [TestMethod]
        public void BoardIdBelowOneThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => new BoardTileDto(1, 0, 1, 1, true, "", 0), BoardTileDto.BOARD_ID_BELOW_ONE);
        }
        
        [TestMethod]
        public void RowBelowOneThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => new BoardTileDto(1, 1, 0, 1, true, "", 0), BoardTileDto.ROW_BELOW_ZERO);
        }
        
        [TestMethod]
        public void ColumnBelowOneThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => new BoardTileDto(1, 1, 1, 0, true, "", 0), BoardTileDto.COLUMN_BELOW_ZERO);
        }
        
        [TestMethod]
        public void NullLetterThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => new BoardTileDto(1, 1, 1, 1, true, null, 0), BoardTileDto.LETTER_IS_NULL);
        }
        
        [TestMethod]
        public void MultipleLettersThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => new BoardTileDto(1, 1, 1, 1, true, "ab", 0), BoardTileDto.LETTER_MORE_THAN_ONE_CHARACTERS);
        }
        
        [TestMethod]
        public void ScoreBelowZeroThrowsException()
        {
            ExceptionAsserter.AssertExceptionWithMessageIsThrown(() => new BoardTileDto(1, 1, 1, 1, true, "a", -1), BoardTileDto.SCORE_BELOW_ZERO);
        }

        [TestMethod]
        public void ToStringIsCorrect()
        {
            Assert.IsTrue(new BoardTileDto(1, 2, 3, 4, true, "a", 5).ToString().Equals(
                "Id = 1, BoardId = 2, Row = 3, Column = 4, IsStart = True, Letter = A, Score = 5"
            ));
        }
    }
}
