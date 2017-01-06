using System.Collections.Generic;
using CheckersLibrary.Cells;
using CheckersLibrary.Moves;

namespace CheckersLibrary.Checkers.Implementations
{
    /// <summary>
    /// Abstract class that contains checker nature implementation
    /// </summary>
    public abstract class CheckerImpl
    {
        /// <summary>
        /// Cell on table witch contains this checker
        /// </summary>
        public Cell Cell { get; set; }
        /// <summary>
        /// List of alowed moves for this checker
        /// </summary>
        public List<Move> AllowedMoves { get; set; } = new List<Move>();
        /// <summary>
        /// Direction of moves
        /// </summary>
        public PlayerMoveDirection PlayerMoveDir { get; }
        /// <summary>
        /// Default constructor for checker nature implemenattion
        /// </summary>
        /// <param name="playerMoveDir">Direction of moves</param>
        protected CheckerImpl(PlayerMoveDirection playerMoveDir)
        {
            PlayerMoveDir = playerMoveDir;
        }

        public abstract bool CheckForwardLeft();
        public abstract bool CheckForwardRight();
        public abstract bool CheckBackwardLeft();
        public abstract bool CheckBackwardRight();
        public abstract bool CheckKillForwardLeft(out Checker killed);
        public abstract bool CheckKillForwardRight(out Checker killed);
        public abstract bool CheckKillBackwardLeft(out Checker killed);
        public abstract bool CheckKillBackwardRight(out Checker killed);
        public abstract Cell[] GetCellAfterKillForwardLeft(Checker killed);
        public abstract Cell[] GetCellAfterKillForwardRight(Checker killed);
        public abstract Cell[] GetCellAfterKillBackwardLeft(Checker killed);
        public abstract Cell[] GetCellAfterKillBackwardRight(Checker killed);
        public abstract void AddMoveForwardLeft(Checker checker);
        public abstract void AddMoveForwardRight(Checker checker);
        public abstract void AddMoveBackwardLeft(Checker checker);
        public abstract void AddMoveBackwardRight(Checker checker);
        public abstract void AddMoveForwardLeft(Checker checker, Checker[] killed, out Move move);
        public abstract void AddMoveForwardRight(Checker checker, Checker[] killed, out Move move);
        public abstract void AddMoveBackwardLeft(Checker checker, Checker[] killed, out Move move);
        public abstract void AddMoveBackwardRight(Checker checker, Checker[] killed, out Move move);
    }
}
