using System;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckersTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWhiteSimpleGo()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] {3, 3};
            Game.StartTestlGame(whiteChs, new int[0][]);
            int counter = 0;
            foreach (Move move in Game.allowedMoves)
            {
                if (move.checker == Game.chsWhite[0] && move.toCell == Table.cells[2, 2])
                    counter++;
                if (move.checker == Game.chsWhite[0] && move.toCell == Table.cells[4, 2])
                    counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestWhiteSimpleForvardKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 3, 3 };
            int[][] blackChs = new int[2][];
            blackChs[0] = new[] { 2, 2 };
            blackChs[1] = new[] { 4, 2 };
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in Game.allowedMoves)
            {
                if (move.checker == Game.chsWhite[0] && move.toCell == Table.cells[1, 1])
                    if(move.killedChs.Contains(Table.getChecker(Table.cells[2, 2])))
                        counter++;
                if (move.checker == Game.chsWhite[0] && move.toCell == Table.cells[5, 1])
                    if (move.killedChs.Contains(Table.getChecker(Table.cells[4, 2])))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestBlackSimpleGo()
        {
            int[][] blackChs = new int[1][];
            blackChs[0] = new[] { 3, 3 };
            Game.StartTestlGame(new int[0][], blackChs, false);
            int counter = 0;
            foreach (Move move in Game.allowedMoves)
            {
                if (move.checker == Game.chsBlack[0] && move.toCell == Table.cells[4, 4])
                    counter++;
                if (move.checker == Game.chsBlack[0] && move.toCell == Table.cells[2, 4])
                    counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestBlackSimpleForvardKill()
        {
            int[][] blackChs = new int[1][];
            blackChs[0] = new[] { 3, 3 };
            int[][] whiteChs = new int[2][];
            whiteChs[0] = new[] { 4, 4 };
            whiteChs[1] = new[] { 2, 4 };
            Game.StartTestlGame(whiteChs, blackChs, false);
            int counter = 0;
            foreach (Move move in Game.allowedMoves)
            {
                if (move.checker == Game.chsBlack[0] && move.toCell == Table.cells[5, 5])
                    if (move.killedChs.Contains(Table.getChecker(Table.cells[4, 4])))
                        counter++;
                if (move.checker == Game.chsBlack[0] && move.toCell == Table.cells[1, 5])
                    if (move.killedChs.Contains(Table.getChecker(Table.cells[2, 4])))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
    }
}
