using System.Collections.Generic;
using CheckersLibrary.Cells;
using CheckersLibrary.Checkers;
using CheckersLibrary.GraphicalImplementation;
using CheckersLibrary.Moves;

namespace CheckersLibrary
{
    /// <summary>
    /// Standart click delegate
    /// </summary>
    public delegate void Click();

    /// <summary>
    /// End of game delegate
    /// </summary>
    /// <param name="color">Color of comand that won the game</param>
    public delegate void EndGame(Color color);

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
        /// End of game event
        /// </summary>
        public static event EndGame EndGame;

        /// <summary>
        /// Start new game
        /// </summary>
        public static void StartNormalGame(CheckerGraphicalImplementation[,,] graphicalImplementation)
        {
            Initialize();

            TableCells.Draw();

            DisposeNormalWhite(graphicalImplementation);

            ComputeMovesWhite();
        }

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
                Checker checker = (isWhite) ? (Checker) new WhiteChecker(wCell, MoveDirection.BottomTop, null): new BlackChecker(wCell, MoveDirection.TopBottom, null);
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
                AllowedMovesCalculator.AllowedMovesCalculator.Calculate(ch);
        }
        /// <summary>
        /// Compute moves for black checkers on table
        /// </summary>
        private static void ComputeMovesBlack()
        {
            foreach (Checker ch in GameDataHandler.BlackCheckers)
                AllowedMovesCalculator.AllowedMovesCalculator.Calculate(ch);
        }
        /// <summary>
        /// Dispose checkers on table, white locate down
        /// </summary>
        private static void DisposeNormalWhite(CheckerGraphicalImplementation[,,] graphicalImplementation)
        {
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    Cell wCell = TableCells.Cell[i % 2 + 2*j, 7 - i];
                    WhiteChecker wChecker = new WhiteChecker(wCell, MoveDirection.BottomTop, graphicalImplementation[0, i, j]);
                    GameDataHandler.WhiteCheckers.Add(wChecker);
                    Cell bCell = TableCells.Cell[(i + 1) % 2 + 2 * j, i];
                    BlackChecker bChecker = new BlackChecker(bCell, MoveDirection.TopBottom, graphicalImplementation[1, i, j]);
                    GameDataHandler.BlackCheckers.Add(bChecker);
                }
        }
        /// <summary>
        /// Check is end of game
        /// </summary>
        /// <returns>true if it's end, false if not</returns>
        public static bool CheckEndOfGame()
        {
            if (GameDataHandler.BlackCheckers.Count == 0)
            {
                Clear();
                EndGame?.Invoke(Color.White);
                return true;
            }
            if (GameDataHandler.WhiteCheckers.Count == 0)
            {
                Clear();
                EndGame?.Invoke(Color.Black);
                return true;
            }
            return false;
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