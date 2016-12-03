using System.Collections.Generic;
using checkers.Cells;
using checkers.Checkers;

namespace checkers.Moves
{
    public class MoveForwardRight : Move
    {
        public MoveForwardRight(Checker checker)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.X + 1 : checker.Cell.X - 1,
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.Y - 1 : checker.Cell.Y + 1])
        { }
        public MoveForwardRight(Checker checker, List<Checker> killed)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.X + 2 : checker.Cell.X - 2,
                    (checker.MoveDir == MoveDirection.BottomTop) ? checker.Cell.Y - 2 : checker.Cell.Y + 2],
                killed)
        { }
        public MoveForwardRight(Checker checker, Cell toCell, List<Checker> killed)
            : base(checker, toCell, killed) { }
        public MoveForwardRight(Checker checker, Cell toCell): base(checker, toCell) { }
    }
}