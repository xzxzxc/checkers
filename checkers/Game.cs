using System.Collections.Generic;
using System.Windows.Forms;

namespace checkers
{
    /// <summary>
    /// Class for controling the game (for implementation save/load functionality in future)
    /// </summary>
    public static class Game
    {
        public static bool isWhite; // for controling whose turn
        public static List<WhiteChecker> chsWhite; // list of white checkers on table
        public static List<BlackChecker> chsBlack; // list of black checkers on table
        public static List<Move> allowedMoves; // list of alowed moves for all checkers
        /// <summary>
        /// Start new game
        /// </summary>
        /// <param name="whiteCheckersCoordinates"></param>
        /// <param name="blackCheckersCoordinates"></param>
        public static void StartTestlGame(int[][] whiteCheckersCoordinates, int[][] blackCheckersCoordinates, bool isWhite=true)
        {
            Game.isWhite = isWhite;
            chsWhite = new List<WhiteChecker>();
            chsBlack = new List<BlackChecker>();
            allowedMoves = new List<Move>();
            Table.CreateTable();

            DisposeTestWhite(whiteCheckersCoordinates);
            DisposeTestBlack(blackCheckersCoordinates);

            if (isWhite)
                ComputeMovesWhite();
            else 
                ComputeMovesBlack();
        }
        private static void DisposeTest(int[][] checkersCoordinates, bool isWhite)
        {
            foreach (int[] checkerCoordinates in checkersCoordinates)
            {
                Cell wCell = Table.cells[checkerCoordinates[0], checkerCoordinates[1]];
                Checker checker = (isWhite) ? new WhiteChecker(wCell): new BlackChecker(wCell) as Checker;
                wCell.checker = checker;
                if(isWhite)
                    chsWhite.Add(checker as WhiteChecker);
                else
                    chsBlack.Add(checker as BlackChecker);
            }
        }

        private static void DisposeTestWhite(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, true);
        }
        private static void DisposeTestBlack(int[][] checkersCoordinates)
        {
            DisposeTest(checkersCoordinates, false);
        }
        /// <summary>
        /// Start new game
        /// </summary>
        public static void StartNormalGame()
        {
            chsWhite = new List<WhiteChecker>();
            chsBlack = new List<BlackChecker>();
            isWhite = true;
            allowedMoves = new List<Move>();
            Table.CreateTable();
            
            DisposeNormalWhite();

            ComputeMovesWhite();
        }
        /// <summary>
        /// Compute moves for all checkers on table
        /// </summary>
        private static void ComputeMovesWhite()
        {
            foreach (Checker ch in chsWhite)
                ch.computeAllowedMoves();
        }
        private static void ComputeMovesBlack()
        {
            foreach (Checker ch in chsBlack)
                ch.computeAllowedMoves();
        }
        /// <summary>
        /// Dispose checkers on teble, white locate down
        /// </summary>
        private static void DisposeNormalWhite()
        {
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    Cell wCell = Table.cells[2*j + i%2, 7 - i];
                    WhiteChecker wChecker = new WhiteChecker(wCell);
                    wCell.checker = wChecker;
                    chsWhite.Add(wChecker);
                    Cell bCell = Table.cells[1 + 2*j - i%2, i];
                    BlackChecker bChecker = new BlackChecker(bCell);
                    bCell.checker = bChecker;
                    chsBlack.Add(bChecker);
                }
        }

        public static void EndGame()
        {
            chsWhite = null;
            chsBlack = null;
            allowedMoves = null;
        }
    }
}