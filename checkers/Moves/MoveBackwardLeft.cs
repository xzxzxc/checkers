using System.Collections.Generic;
using checkers.Cells;
using checkers.Checkers;

namespace checkers.Moves
{
    public class MoveBackwardLeft : Move
    {
        public MoveBackwardLeft(Checker checker, List<Checker> killed)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.X - 2 : checker.Cell.X + 2,
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.Y + 2 : checker.Cell.Y - 2],
                killed)
        { }
        public MoveBackwardLeft(Checker checker, Cell toCell, List<Checker> killed)
            : base(checker, toCell, killed) { }
        public MoveBackwardLeft(Checker checker, Cell toCell): base(checker, toCell) { }
    }
}