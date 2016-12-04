using System.Collections.Generic;
using System.Windows.Forms;
using checkers.Cells;
using checkers.Checkers;
using checkers.Moves;

namespace checkers
{
    /// <summary>
    /// Standart click delegate
    /// </summary>
    public delegate void Click();

    /// <summary>
    /// Class for controling the game (for implementation save/load functionality in future)
    /// </summary>
    public static class Game
    {
        /// <summary>
        /// True if it's white turn, false if not
        /// </summary>
        public static bool IsWhite;

        /// <summary>
        /// Start new test game
        /// </summary>
        /// <param name="whiteCheckersCoordinates"></param>
        /// <param name="blackCheckersCoordinates"></param>
        /// <param name="whiteQeensCoordinates"></param>
        /// <param name="blackQueensCoordinates"></param>
        /// <param name="isWhite"></param>
        public static void StartTestlGame(int[][] whiteCheckersCoordinates, int[][] blackCheckersCoordinates, int[][] whiteQeensCoordinates=null, int[][] blackQueensCoordinates=null, bool isWhite=true)
        {
            Game.IsWhite = isWhite;
            GameDataHandler.WhiteCheckers = new List<WhiteChecker>();
            GameDataHandler.BlackCheckers = new List<BlackChecker>();

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
        /// <summary>
        /// Dispose checkers in test game
        /// </summary>
        /// <param name="checkersCoordinates"></param>
        /// <param name="isWhite"></param>
        /// <param name="isQueen"></param>
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
                    GameDataHandler.WhiteCheckers.Add((WhiteChecker) checker);
                else
                    GameDataHandler.BlackCheckers.Add((BlackChecker) checker);
            }
        }
        /// <summary>
        /// Dispose white checkers
        /// </summary>
        /// <param name="checkersCoordinates"></param>
        private static void DisposeTestWhite(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, true);
        }
        /// <summary>
        /// Dispose white queens
        /// </summary>
        /// <param name="checkersCoordinates"></param>
        private static void DisposeTestWhiteQueens(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, true, true);
        }
        /// <summary>
        /// Dispose black checkers
        /// </summary>
        /// <param name="checkersCoordinates"></param>
        private static void DisposeTestBlack(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, false);
        }
        /// <summary>
        /// Dispose black queens
        /// </summary>
        /// <param name="checkersCoordinates"></param>
        private static void DisposeTestBlackQueens(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, false, true);
        }

        /// <summary>
        /// Start new game
        /// </summary>
        public static void StartNormalGame()
        {
            Initialize();

            TableCells.Draw();

            DisposeNormalWhite();

            ComputeMovesWhite();
        }
        /// <summary>
        /// Initialize variables
        /// </summary>
        private static void Initialize()
        {
            GameDataHandler.WhiteCheckers = new List<WhiteChecker>();
            GameDataHandler.BlackCheckers = new List<BlackChecker>();
            GameDataHandler.PreviousMoves = new List<Move>();
            IsWhite = true;
            GameDataHandler.CurrentMoveIndex = 0;
        }

        /// <summary>
        /// Compute moves for white checkers on table
        /// </summary>
        private static void ComputeMovesWhite()
        {
            foreach (Checker ch in GameDataHandler.WhiteCheckers)
                AllowedMovesCalculator.Calculate(ch);
        }
        /// <summary>
        /// Compute moves for black checkers on table
        /// </summary>
        private static void ComputeMovesBlack()
        {
            foreach (Checker ch in GameDataHandler.BlackCheckers)
                AllowedMovesCalculator.Calculate(ch);
        }
        /// <summary>
        /// Dispose checkers on table, white locate down
        /// </summary>
        private static void DisposeNormalWhite()
        {
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    Cell wCell = TableCells.Cell[i % 2 + 2*j, 7 - i];
                    WhiteChecker wChecker = new WhiteChecker(wCell, MoveDirection.BottomTop);
                    GameDataHandler.WhiteCheckers.Add(wChecker);
                    Cell bCell = TableCells.Cell[(i + 1) % 2 + 2 * j, i];
                    BlackChecker bChecker = new BlackChecker(bCell, MoveDirection.TopBottom);
                    GameDataHandler.BlackCheckers.Add(bChecker);
                }
        }
        /// <summary>
        /// Check is end of game
        /// </summary>
        /// <returns>true if it's end, false if not</returns>
        public static bool CheckEndOfGame()
        {
            return GameDataHandler.BlackCheckers.Count == 0 || GameDataHandler.WhiteCheckers.Count == 0;
        }
        /// <summary>
        /// Do end game
        /// </summary>
        public static void EndGame()
        {
            MessageBox.Show(GameDataHandler.BlackCheckers.Count == 0? "White win":"Black win", "Game is end");
            Clear();
        }
        /// <summary>
        /// Delete game data
        /// </summary>
        public static void Clear()
        {
            GameDataHandler.WhiteCheckers = null;
            GameDataHandler.BlackCheckers = null;
            GameDataHandler.PreviousMoves = null;
            GameDataHandler.CurrentMoveIndex = 0;
            GameDataHandler.Selected = null;
            TableCells.Clear();
        }

        /// <summary>
        /// Undo previous move
        /// </summary>
        public static void Undo()
        {
            if (GameDataHandler.PreviousMoves?.Count != 0 && GameDataHandler.CurrentMoveIndex != 0)
            {
                GameDataHandler.PreviousMoves[GameDataHandler.CurrentMoveIndex - 1].Undo();
                GameDataHandler.CurrentMoveIndex--;
            }
        }
        /// <summary>
        /// Redo previous move
        /// </summary>
        public static void Redo()
        {
            if (GameDataHandler.PreviousMoves != null && GameDataHandler.PreviousMoves.Count != 0 && GameDataHandler.CurrentMoveIndex < GameDataHandler.PreviousMoves.Count)
            {
                GameDataHandler.PreviousMoves[GameDataHandler.CurrentMoveIndex].Redo();
                GameDataHandler.CurrentMoveIndex++;
            }
        }
        /// <summary>
        /// Increment index of current move
        /// </summary>
        public static void IncrimentIndex()
        {
            GameDataHandler.CurrentMoveIndex++;
        }
        /// <summary>
        /// Unselect selected checker
        /// </summary>
        public static void Unselect()
        {
            GameDataHandler.Selected?.Unselect();
        }
    }
}