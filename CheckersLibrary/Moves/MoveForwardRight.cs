using System.Collections.Generic;
using CheckersLibrary.Cells;
using CheckersLibrary.Checkers;

namespace CheckersLibrary.Moves
{
    public class MoveForwardRight : Move
    {
        public MoveForwardRight(Checker checker)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDirection == MoveDirection.BottomTop) ? checker.Cell.X + 1 : checker.Cell.X - 1,
                    (checker.MoveDirection == MoveDirection.BottomTop) ? checker.Cell.Y - 1 : checker.Cell.Y + 1])
        { }
        public MoveForwardRight(Checker checker, List<Checker> killed)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDirection == MoveDirection.BottomTop) ? checker.Cell.X + 2 : checker.Cell.X - 2,
                    (checker.MoveDirection == MoveDirection.BottomTop) ? checker.Cell.Y - 2 : checker.Cell.Y + 2],
                killed)
        { }
        public MoveForwardRight(Checker checker, Cell toCell, List<Checker> killed)
            : base(checker, toCell, killed) { }
        public MoveForwardRight(Checker checker, Cell toCell): base(checker, toCell) { }
    }
}