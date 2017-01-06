using System.Collections.Generic;
using CheckersLibrary.Checkers;
using CheckersLibrary.Moves;

namespace CheckersLibrary
{
    public struct GameDataHandler
    {
        /// <summary>
        /// List of previous moves
        /// </summary>
        public static List<Move> PreviousMoves;
        /// <summary>
        /// Index of current move in previous moves list
        /// </summary>
        public static int CurrentMoveIndex;
        /// <summary>
        /// List of white checkers on table
        /// </summary>
        public static List<WhiteChecker> WhiteCheckers;
        /// <summary>
        /// List of black checkers on table
        /// </summary>
        public static List<BlackChecker> BlackCheckers;
        /// <summary>
        /// Current selected checker
        /// </summary>
        public static Checker Selected = null;

        public static void AddMove(Move move)
        {
            if (CurrentMoveIndex != PreviousMoves.Count)
            {
                PreviousMoves.RemoveRange(CurrentMoveIndex, PreviousMoves.Count - CurrentMoveIndex);
            }
            PreviousMoves.Add(move);
            Game.IncrimentIndex();
        }

    }
}
