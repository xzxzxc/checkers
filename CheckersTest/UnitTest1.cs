using System;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using checkers;
using checkers.Cells;
using checkers.Moves;
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
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[2, 2])
                    counter++;
                if (move.toCell == TableCells.Cell[4, 2])
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
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[1, 1])
                    if(move.killed.Contains(TableCells.Cell[2, 2].Checker))
                        counter++;
                if (move.toCell == TableCells.Cell[5, 1])
                    if (move.killed.Contains(TableCells.Cell[4, 2].Checker))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestWhiteSimpleBackwardKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 3, 3 };
            int[][] blackChs = new int[2][];
            blackChs[0] = new[] { 4, 4 };
            blackChs[1] = new[] { 2, 4 };
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[5, 5])
                    if (move.killed.Contains(TableCells.Cell[4, 4].Checker))
                        counter++;
                if (move.toCell == TableCells.Cell[1, 5])
                    if (move.killed.Contains(TableCells.Cell[2, 4].Checker))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestWhiteDoubleKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 4, 4 };
            int[][] blackChs = new int[2][];
            blackChs[0] = new[] { 3, 3 };
            blackChs[1] = new[] { 1, 1 };
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[2, 2])
                    if (move.killed.Contains(TableCells.Cell[3, 3].Checker))
                        counter++;
                if (move.toCell == TableCells.Cell[0, 0])
                    if (move.killed.Contains(TableCells.Cell[1, 1].Checker))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestWhiteTrippleKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 6, 6 };
            int[][] blackChs = new int[3][];
            blackChs[0] = new[] { 3, 3 };
            blackChs[1] = new[] { 1, 1 };
            blackChs[2] = new[] { 5, 5 };
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[2, 2])
                    if (move.killed.Contains(TableCells.Cell[3, 3].Checker))
                        counter++;
                if (move.toCell == TableCells.Cell[0, 0])
                    if (move.killed.Contains(TableCells.Cell[1, 1].Checker))
                        counter++;
                if (move.toCell == TableCells.Cell[4, 4])
                    if (move.killed.Contains(TableCells.Cell[5, 5].Checker))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 3);
        }
        [TestMethod]
        public void TestWhiteQueenGo()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 4, 4 };
            Game.StartTestlGame(new int[0][], new int[0][], whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[2, 2])
                    counter++;
                if (move.toCell == TableCells.Cell[6, 2])
                    counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestWhiteQueenKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 6, 6 };
            int[][] blackChs = new int[1][];
            blackChs[0] = new[] { 4, 4 };
            Game.StartTestlGame(new int[0][], blackChs, whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[3, 3])
                    if (move.killed.Contains(TableCells.Cell[4, 4].Checker))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 1);
        }
        [TestMethod]
        public void TestWhiteQueenDoubleKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 6, 6 };
            int[][] blackChs = new int[2][];
            blackChs[0] = new[] { 4, 4 };
            blackChs[1] = new[] { 5, 1 };
            Game.StartTestlGame(new int[0][], blackChs, whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[6, 0])
                    if (move.killed.Contains(TableCells.Cell[5, 1].Checker))
                        if(move.killed.Contains(TableCells.Cell[4, 4].Checker))
                            counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 1);
        }
        [TestMethod]
        public void TestWhiteQueenTrippleKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 2, 5 };
            int[][] blackChs = new int[3][];
            blackChs[0] = new[] { 1, 4 };
            blackChs[1] = new[] { 2, 1 };
            blackChs[2] = new[] { 5, 2 };
            Game.StartTestlGame(new int[0][], blackChs, whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsWhite[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[7, 4])
                    if (move.killed.Contains(TableCells.Cell[1, 4].Checker))
                        if (move.killed.Contains(TableCells.Cell[2, 1].Checker))
                            if (move.killed.Contains(TableCells.Cell[5, 2].Checker))
                                counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 1);
        }
        [TestMethod]
        public void TestBlackSimpleGo()
        {
            int[][] blackChs = new int[1][];
            blackChs[0] = new[] { 3, 3 };
            Game.StartTestlGame(new int[0][], blackChs, isWhite:false);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsBlack[0].allowedMoves)
            {
                if (move.toCell == TableCells.Cell[4, 4])
                    counter++;
                if ( move.toCell == TableCells.Cell[2, 4])
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
            Game.StartTestlGame(whiteChs, blackChs, isWhite:false);
            int counter = 0;
            foreach (Move move in GameDataHandler.chsBlack[0].allowedMoves)
            {
                if ( move.toCell == TableCells.Cell[5, 5])
                    if (move.killed.Contains(TableCells.Cell[4, 4].Checker))
                        counter++;
                if ( move.toCell == TableCells.Cell[1, 5])
                    if (move.killed.Contains(TableCells.Cell[2, 4].Checker))
                        counter++;
            }
            Game.EndGame();
            Assert.AreEqual(counter, 2);
        }
        
    }
}
