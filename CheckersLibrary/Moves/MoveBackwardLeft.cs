using System.Collections.Generic;
using CheckersLibrary.Cells;
using CheckersLibrary.Checkers;

namespace CheckersLibrary.Moves
{
    public class MoveBackwardLeft : Move
    {
        public MoveBackwardLeft(Checker checker, List<Checker> killed)
            : base(
                checker,
                TableCells.Cell[
                    (checker.MoveDirection == MoveDirection.BottomTop) ? checker.Cell.X - 2 : checker.Cell.X + 2,
                    (checker.MoveDirection == MoveDirection.BottomTop) ? checker.Cell.Y + 2 : checker.Cell.Y - 2],
                killed)
        { }
        public MoveBackwardLeft(Checker checker, Cell toCell, List<Checker> killed)
            : base(checker, toCell, killed) { }
        public MoveBackwardLeft(Checker checker, Cell toCell): base(checker, toCell) { }
    }
}