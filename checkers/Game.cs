using System.Collections.Generic;
using System.Windows.Forms;
using checkers.Cells;
using checkers.Checkers;
using checkers.Moves;

namespace checkers
{
    /// <summary>
    /// Class for controling the game (for implementation save/load functionality in future)
    /// </summary>
    public static class Game
    {
        public static bool isWhite; // for controling whose turn

        /// <summary>
        /// Start new game
        /// </summary>
        /// <param name="whiteCheckersCoordinates"></param>
        /// <param name="blackCheckersCoordinates"></param>
        public static void StartTestlGame(int[][] whiteCheckersCoordinates, int[][] blackCheckersCoordinates, int[][] whiteQeensCoordinates=null, int[][] blackQueensCoordinates=null, bool isWhite=true)
        {
            Game.isWhite = isWhite;
            GameDataHandler.chsWhite = new List<WhiteChecker>();
            GameDataHandler.chsBlack = new List<BlackChecker>();

            DisposeTestWhite(whiteCheckersCoordinates);
            DisposeTestBlack(blackCheckersCoordinates);
            if(whiteQeensCoordinates!=null)
                DisposeTestWhiteQueens(whiteQeensCoordinates);
            if (blackQueensCoordinates != null)
                DisposeTestBlackQueens(blackQueensCoordinates);

            if (isWhite)
                ComputeMovesWhite();
            else 
                ComputeMovesBlack();
        }
        private static void DisposeTest(int[][] checkersCoordinates, bool isWhite, bool isQueen=false)
        {
            foreach (int[] checkerCoordinates in checkersCoordinates)
            {
                Cell wCell = TableCells.Cell[checkerCoordinates[0], checkerCoordinates[1]];
                Checker checker = (isWhite) ? (Checker) new WhiteChecker(wCell, MoveDirection.BottomTop): new BlackChecker(wCell, MoveDirection.TopBottom);
                if (isQueen)
                    checker.BeQueen();
                // wCell.Checker = checker;
                if (isWhite)
                    GameDataHandler.chsWhite.Add((WhiteChecker) checker);
                else
                    GameDataHandler.chsBlack.Add((BlackChecker) checker);
            }
        }

        private static void DisposeTestWhite(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, true);
        }
        private static void DisposeTestWhiteQueens(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, true, true);
        }
        private static void DisposeTestBlack(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, false);
        }
        private static void DisposeTestBlackQueens(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, false, true);
        }

        /// <summary>
        /// Start new game
        /// </summary>
        public static void StartNormalGame()
        {
            GameDataHandler.chsWhite = new List<WhiteChecker>();
            GameDataHandler.chsBlack = new List<BlackChecker>();
            isWhite = true;

            TableCells.Draw();

            DisposeNormalWhite();

            ComputeMovesWhite();
        }
        /// <summary>
        /// Compute moves for all checkers on table
        /// </summary>
        private static void ComputeMovesWhite()
        {
            foreach (Checker ch in GameDataHandler.chsWhite)
                AllowedMovesCalculator.Calculate(ch);
        }
        private static void ComputeMovesBlack()
        {
            foreach (Checker ch in GameDataHandler.chsBlack)
                AllowedMovesCalculator.Calculate(ch);
        }
        /// <summary>
        /// Dispose checkers on teble, white locate down
        /// </summary>
        private static void DisposeNormalWhite()
        {
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    Cell wCell = TableCells.Cell[i % 2 + 2*j, 7 - i];
                    WhiteChecker wChecker = new WhiteChecker(wCell, MoveDirection.BottomTop);
                    GameDataHandler.chsWhite.Add(wChecker);
                    Cell bCell = TableCells.Cell[(i + 1) % 2 + 2 * j, i];
                    BlackChecker bChecker = new BlackChecker(bCell, MoveDirection.TopBottom);
                    GameDataHandler.chsBlack.Add(bChecker);
                }
        }

        public static bool CheckEndOfGame()
        {
            return GameDataHandler.chsBlack.Count == 0 || GameDataHandler.chsWhite.Count == 0;
        }
        public static void EndGame()
        {
            // MessageBox.Show(GameDataHandler.chsBlack.Count == 0? "White win":"Black win", "Game is end");
            GameDataHandler.chsWhite = null;
            GameDataHandler.chsBlack = null;
            TableCells.Clear();

        }
    }
}