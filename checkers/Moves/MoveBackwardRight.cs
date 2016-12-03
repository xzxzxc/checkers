using System.Collections.Generic;
using checkers.Cells;
using checkers.Checkers;

namespace checkers.Moves
{
    public class MoveBackwardRight : Move
    {
        public MoveBackwardRight(Checker checker, List<Checker> killed)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.X + 2 : checker.Cell.X - 2,
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.Y + 2 : checker.Cell.Y - 2],
                killed)
        { }
        public MoveBackwardRight(Checker checker, Cell toCell, List<Checker> killed)
            : base(checker, toCell, killed) { }
        public MoveBackwardRight(Checker checker, Cell toCell): base(checker, toCell) { }
    }
}