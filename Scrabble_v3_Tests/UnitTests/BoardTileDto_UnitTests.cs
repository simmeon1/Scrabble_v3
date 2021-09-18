using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble_v3_ClassLibrary.DataObjects;

namespace Scrabble_v3_Tests.UnitTests
{
    [TestClass]
    public class BoardTileDto_UnitTests
    {
        [TestMethod]
        public void ToStringIsCorrect()
        {
            Assert.IsTrue(new BoardTileDto(1, 2, 3, 4, true).ToString().Equals("Id = 1, BoardId = 2, Row = 3, Column = 4, IsStart = True"));
        }
    }
}
