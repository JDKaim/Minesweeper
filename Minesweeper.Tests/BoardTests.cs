using System;
using Minesweeper.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void IsOnBoard()
        {
            Board board = new Board(5, 10);
            board.VerifyIsOnBoard(4, 9);
        }
         
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotOnBoard()
        {
            Board board = new Board(5, 10);
            board.VerifyIsOnBoard(-1, 9);
        }
    }
}
