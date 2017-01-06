using System.Linq;
using CheckersLibrary;
using CheckersLibrary.Cells;
using CheckersLibrary.GraphicalImplementation;
using CheckersLibrary.Moves;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckersTest
{
    [TestClass]
    public class UnitTest1
    {
        private CellGraphicalImplementation[,] GenerateArrayCellImplementations()
        {
            CellGraphicalImplementation[,] testCellImplementations = new CellGraphicalImplementation[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    testCellImplementations[i, j] = new TestCellImplementation();
                }
            }
            return testCellImplementations;
        }

        [TestMethod]
        public void TestWhiteSimpleGo()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] {3, 3};
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(whiteChs, new int[0][]);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[2, 2])
                    counter++;
                if (move.ToCell == TableCells.Cell[4, 2])
                    counter++;
            }
            Game.Clear();
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
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[1, 1])
                    if(move.Killed.Contains(TableCells.Cell[2, 2].Checker))
                        counter++;
                if (move.ToCell == TableCells.Cell[5, 1])
                    if (move.Killed.Contains(TableCells.Cell[4, 2].Checker))
                        counter++;
            }
            Game.Clear();
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
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[5, 5])
                    if (move.Killed.Contains(TableCells.Cell[4, 4].Checker))
                        counter++;
                if (move.ToCell == TableCells.Cell[1, 5])
                    if (move.Killed.Contains(TableCells.Cell[2, 4].Checker))
                        counter++;
            }
            Game.Clear();
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
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[2, 2])
                    if (move.Killed.Contains(TableCells.Cell[3, 3].Checker))
                        counter++;
                if (move.ToCell == TableCells.Cell[0, 0])
                    if (move.Killed.Contains(TableCells.Cell[1, 1].Checker))
                        counter++;
            }
            Game.Clear();
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
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(whiteChs, blackChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[2, 2])
                    if (move.Killed.Contains(TableCells.Cell[3, 3].Checker))
                        counter++;
                if (move.ToCell == TableCells.Cell[0, 0])
                    if (move.Killed.Contains(TableCells.Cell[1, 1].Checker))
                        counter++;
                if (move.ToCell == TableCells.Cell[4, 4])
                    if (move.Killed.Contains(TableCells.Cell[5, 5].Checker))
                        counter++;
            }
            Game.Clear();
            Assert.AreEqual(counter, 3);
        }
        [TestMethod]
        public void TestWhiteQueenGo()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 4, 4 };
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(new int[0][], new int[0][], whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[2, 2])
                    counter++;
                if (move.ToCell == TableCells.Cell[6, 2])
                    counter++;
            }
            Game.Clear();
            Assert.AreEqual(counter, 2);
        }
        [TestMethod]
        public void TestWhiteQueenKill()
        {
            int[][] whiteChs = new int[1][];
            whiteChs[0] = new[] { 6, 6 };
            int[][] blackChs = new int[1][];
            blackChs[0] = new[] { 4, 4 };
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(new int[0][], blackChs, whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[3, 3])
                    if (move.Killed.Contains(TableCells.Cell[4, 4].Checker))
                        counter++;
            }
            Game.Clear();
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
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(new int[0][], blackChs, whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[6, 0])
                    if (move.Killed.Contains(TableCells.Cell[5, 1].Checker))
                        if(move.Killed.Contains(TableCells.Cell[4, 4].Checker))
                            counter++;
            }
            Game.Clear();
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
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(new int[0][], blackChs, whiteChs);
            int counter = 0;
            foreach (Move move in GameDataHandler.WhiteCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[7, 4])
                    if (move.Killed.Contains(TableCells.Cell[1, 4].Checker))
                        if (move.Killed.Contains(TableCells.Cell[2, 1].Checker))
                            if (move.Killed.Contains(TableCells.Cell[5, 2].Checker))
                                counter++;
            }
            Game.Clear();
            Assert.AreEqual(counter, 1);
        }
        [TestMethod]
        public void TestBlackSimpleGo()
        {
            int[][] blackChs = new int[1][];
            blackChs[0] = new[] { 3, 3 };
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(new int[0][], blackChs, isWhite:false);
            int counter = 0;
            foreach (Move move in GameDataHandler.BlackCheckers[0].AllowedMoves)
            {
                if (move.ToCell == TableCells.Cell[4, 4])
                    counter++;
                if ( move.ToCell == TableCells.Cell[2, 4])
                    counter++;
            }
            Game.Clear();
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
            TableCells.Create(GenerateArrayCellImplementations());
            Game.StartTestlGame(whiteChs, blackChs, isWhite:false);
            int counter = 0;
            foreach (Move move in GameDataHandler.BlackCheckers[0].AllowedMoves)
            {
                if ( move.ToCell == TableCells.Cell[5, 5])
                    if (move.Killed.Contains(TableCells.Cell[4, 4].Checker))
                        counter++;
                if ( move.ToCell == TableCells.Cell[1, 5])
                    if (move.Killed.Contains(TableCells.Cell[2, 4].Checker))
                        counter++;
            }
            Game.Clear();
            Assert.AreEqual(counter, 2);
        }
        
    }
}
