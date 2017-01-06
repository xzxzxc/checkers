using System.Collections.Generic;
using CheckersLibrary.AI;
using CheckersLibrary.Cells;
using CheckersLibrary.Checkers;

namespace CheckersLibrary.Moves
{
    /// <summary>
    /// Class for controling possible moves
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Chacker thats moving
        /// </summary>
        private Checker Checker { get; }
        /// <summary>
        /// Where checker move
        /// </summary>
        public Cell ToCell;
        /// <summary>
        /// What checkers will be killed
        /// </summary>
        public Checker[] Killed;
        /// <summary>
        /// From wat cell checker move (using in Undo)
        /// </summary>
        private Cell _fromCell;
        /// <summary>
        /// If true, after move, checker would be queen
        /// </summary>
        public bool SetQueen;

        /// <summary>
        /// Default move constructor
        /// </summary>
        /// <param name="checker">Chacker thats moving</param>
        /// <param name="toCell">Where checker move</param>
        /// <param name="killed">What checkers will be killed</param>
        public Move(Checker checker, Cell toCell, Checker[] killed = null)
        {
            Checker = checker;
            ToCell = toCell;
            Killed = killed;
            SetQueen = false;
            _fromCell = Checker.Cell;
            if (ToCell.Y == (Checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop ? 0 : 7))
                SetQueen = true;
        }

        public void AddSelfToGameMoveList()
        {
            GameDataHandler.AddMove(this);
        }

        /// <summary>
        /// Move checker for real
        /// </summary>
        public void Do()
        {
            Checker.Cell.Clear();
            Game.Unselect();
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
            if (SetQueen)
                Checker.BeQueen();
            if (!Game.VsCpu)
                Game.IsWhite = !Game.IsWhite;
            else
                ArtificialIntelligence.Move();
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
            if (ToCell.Y == (Checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop ? 0 : 7))
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
            if (Checker is WhiteChecker)
                foreach (Checker ch in GameDataHandler.BlackCheckers)
                    AllowedMovesCalculator.AllowedMovesCalculator.Calculate(ch);
            else
                foreach (Checker ch in GameDataHandler.WhiteCheckers)
                    AllowedMovesCalculator.AllowedMovesCalculator.Calculate(ch);
        }
    }
}