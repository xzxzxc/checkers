using System.Collections.Generic;
using checkers.Cells;
using checkers.Checkers;

namespace checkers.Moves
{
    public class MoveForwardLeft : Move
    {
        public MoveForwardLeft(Checker checker)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.X - 1 : checker.Cell.X + 1,
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.Y - 1 : checker.Cell.Y + 1])
        { }
        public MoveForwardLeft(Checker checker, List<Checker> killed)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.X - 2 : checker.Cell.X + 2,
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.Y - 2 : checker.Cell.Y + 2],
                killed)
        { }
        public MoveForwardLeft(Checker checker, Cell toCell, List<Checker> killed)
            : base(checker, toCell, killed) { }
        public MoveForwardLeft(Checker checker, Cell toCell): base(checker, toCell) { }
    }
}