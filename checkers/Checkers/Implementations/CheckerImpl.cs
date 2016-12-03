using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using checkers.Cells;
using checkers.Checkers;
using checkers.Moves;

namespace checkers
{
    public abstract class CheckerImpl
    {
        public Cell Cell { get; set; } // cell on table witch contains this checker
        public List<Move> AllowedMoves { get; set; } = new List<Move>() ; // list of alowed moves for all checkers
        public MoveDirection MoveDir { get; }

        protected CheckerImpl(MoveDirection moveDir)
        {
            MoveDir = moveDir;
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
        public abstract void AddMoveForwardLeft(Checker checker, List<Checker> killed, out Move move);
        public abstract void AddMoveForwardRight(Checker checker, List<Checker> killed, out Move move);
        public abstract void AddMoveBackwardLeft(Checker checker, List<Checker> killed, out Move move);
        public abstract void AddMoveBackwardRight(Checker checker, List<Checker> killed, out Move move);
    }
}
