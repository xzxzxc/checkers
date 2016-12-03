using System;
using System.Collections.Generic;
using System.Windows.Forms;
using checkers.Cells;
using checkers.Checkers;

namespace checkers.Moves
{
    /// <summary>
    /// Class for controling possible moves
    /// </summary>
    public abstract class Move
    {
        public Checker Checker { get; } // what chacker is moving
        public Cell toCell; // where checker move
        public List<Checker> killed; // what checkers will be killed

        protected Move(Checker checker, Cell toCell, List<Checker> killed)
        {
            Checker = checker;
            this.toCell = toCell;
            this.killed = killed;
        }

        protected Move(Checker checker, Cell toCell) : this(checker, toCell, null) { }

        public void Do()
        {
            Checker.Cell.Clear();
            Checker.Unselect();
            Checker.Cell = toCell;
            Checker.ClearMoves();
            if (killed!=null)
                foreach (Checker killedChecker in killed)
                    killedChecker.Kill();
            if(Game.CheckEndOfGame())
                Game.EndGame();
            ComputeAllowedMoves();
            if (IsExistMoveWithKill())
                ClearMovesWithoutCills();
            Game.isWhite = !Game.isWhite;
            if (toCell.Y == (Checker.MoveDir == MoveDirection.BottomTop ? 0 : 7))
            {
                Checker.BeQueen();
            }
        }

        private void ClearMovesWithoutCills()
        {
            if (! Game.isWhite)
            {
                foreach (Checker ch in GameDataHandler.chsWhite)
                    for (int i = ch.allowedMoves.Count - 1; i >= 0; i--)
                        if (ch.allowedMoves[i].killed == null)
                            ch.allowedMoves.Remove(ch.allowedMoves[i]);
            }
            else
            {
                foreach (Checker ch in GameDataHandler.chsBlack)
                    for (int i = ch.allowedMoves.Count - 1; i >= 0; i--)
                        if (ch.allowedMoves[i].killed == null)
                            ch.allowedMoves.Remove(ch.allowedMoves[i]);
            }

        }

        private bool IsExistMoveWithKill()
        {
            if (! Game.isWhite)
            {
                foreach (Checker ch in GameDataHandler.chsWhite)
                    foreach (Move move in ch.allowedMoves)
                        if (move.killed != null)
                            return true;
            }
            else
            {
                foreach (Checker ch in GameDataHandler.chsBlack)
                    foreach (Move move in ch.allowedMoves)
                        if (move.killed != null)
                            return true;
            }
            return false;
        }
        private void ComputeAllowedMoves()
        {
            foreach (Checker ch in GameDataHandler.chsBlack)
                AllowedMovesCalculator.Calculate(ch);
            foreach (Checker ch in GameDataHandler.chsWhite)
                AllowedMovesCalculator.Calculate(ch);
        }
    }
}