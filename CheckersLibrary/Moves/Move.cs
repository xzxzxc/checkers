using System.Collections.Generic;
using CheckersLibrary.Cells;
using CheckersLibrary.Checkers;

namespace CheckersLibrary.Moves
{
    /// <summary>
    /// Class for controling possible moves
    /// </summary>
    public abstract class Move
    {
        /// <summary>
        /// Chacker thats moving
        /// </summary>
        public Checker Checker { get; }
        /// <summary>
        /// Where checker move
        /// </summary>
        public Cell ToCell;
        /// <summary>
        /// What checkers will be killed
        /// </summary>
        public List<Checker> Killed;
        /// <summary>
        /// From wat cell checker move (using in Undo)
        /// </summary>
        private Cell _fromCell;
        /// <summary>
        /// Default move constructor
        /// </summary>
        /// <param name="checker">Chacker thats moving</param>
        /// <param name="toCell">Where checker move</param>
        /// <param name="killed">What checkers will be killed</param>
        protected Move(Checker checker, Cell toCell, List<Checker> killed)
        {
            Checker = checker;
            ToCell = toCell;
            Killed = killed;
            _fromCell = Checker.Cell;
        }
        /// <summary>
        /// Constructor of move without kills
        /// </summary>
        /// <param name="checker">Chacker thats moving</param>
        /// <param name="toCell">Where checker move</param>
        protected Move(Checker checker, Cell toCell) : this(checker, toCell, null) { }
        /// <summary>
        /// Move checker for real
        /// </summary>
        public void Do()
        {
            Checker.Cell.Clear();
            Checker.Unselect();
            Checker.Cell = ToCell;
            Checker.ClearMoves();
            if (Killed!=null)
                foreach (Checker killedChecker in Killed)
                    killedChecker.Kill();
            if(Game.CheckEndOfGame())
                return;
            ComputeAllowedMoves();
            if (IsExistMoveWithKill())
                ClearMovesWithoutCills();
            Game.IsWhite = !Game.IsWhite;
            if (ToCell.Y == (Checker.MoveDirection == MoveDirection.BottomTop ? 0 : 7))
            {
                Checker.BeQueen();
            }
            GameDataHandler.AddMove(this);
            Game.IncrimentIndex();
        }

        public void Redo()
        {
            Checker.Cell.Clear();
            Game.Unselect();
            Checker.Cell = ToCell;
            Checker.ClearMoves();
            if (Killed != null)
                foreach (Checker killedChecker in Killed)
                    killedChecker.Kill();
            if (Game.CheckEndOfGame())
                return;
            ComputeAllowedMoves();
            if (IsExistMoveWithKill())
                ClearMovesWithoutCills();
            Game.IsWhite = !Game.IsWhite;
            if (ToCell.Y == ((Checker.MoveDirection == MoveDirection.BottomTop) ? 0 : 7))
            {
                Checker.BeQueen();
            }
        }

        public void Undo()
        {
            Checker.Cell.Clear();
            Game.Unselect();
            Checker.Cell = _fromCell;
            Checker.ClearMoves();
            if (Killed != null)
                foreach (Checker killedChecker in Killed)
                    killedChecker.Resurect();
            ComputeAllowedMoves();
            if (IsExistMoveWithKill())
                ClearMovesWithoutCills();
            Game.IsWhite = !Game.IsWhite;
            if (ToCell.Y == (Checker.MoveDirection == MoveDirection.BottomTop ? 0 : 7))
            {
                Checker.UnBeQueen();
            }
        }
        /// <summary>
        /// Delete all moves withou kills
        /// </summary>
        private void ClearMovesWithoutCills()
        {
            if (! Game.IsWhite)
            {
                foreach (Checker ch in GameDataHandler.WhiteCheckers)
                    for (int i = ch.AllowedMoves.Count - 1; i >= 0; i--)
                        if (ch.AllowedMoves[i].Killed == null)
                            ch.AllowedMoves.Remove(ch.AllowedMoves[i]);
            }
            else
            {
                foreach (Checker ch in GameDataHandler.BlackCheckers)
                    for (int i = ch.AllowedMoves.Count - 1; i >= 0; i--)
                        if (ch.AllowedMoves[i].Killed == null)
                            ch.AllowedMoves.Remove(ch.AllowedMoves[i]);
            }

        }
        /// <summary>
        /// Ckeck if there is at least one move with kill
        /// </summary>
        /// <returns>True if yes, false if not</returns>
        private bool IsExistMoveWithKill()
        {
            if (! Game.IsWhite)
            {
                foreach (Checker ch in GameDataHandler.WhiteCheckers)
                    foreach (Move move in ch.AllowedMoves)
                        if (move.Killed != null)
                            return true;
            }
            else
            {
                foreach (Checker ch in GameDataHandler.BlackCheckers)
                    foreach (Move move in ch.AllowedMoves)
                        if (move.Killed != null)
                            return true;
            }
            return false;
        }
        /// <summary>
        /// Compute allowed moves for all checkers
        /// </summary>
        private void ComputeAllowedMoves()
        {
            foreach (Checker ch in GameDataHandler.BlackCheckers)
                AllowedMovesCalculator.AllowedMovesCalculator.Calculate(ch);
            foreach (Checker ch in GameDataHandler.WhiteCheckers)
                AllowedMovesCalculator.AllowedMovesCalculator.Calculate(ch);
        }
    }
}