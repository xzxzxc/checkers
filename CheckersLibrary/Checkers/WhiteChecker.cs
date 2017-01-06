using CheckersLibrary.Cells;
using CheckersLibrary.GraphicalImplementation;
using CheckersLibrary.Moves;

namespace CheckersLibrary.Checkers
{
    public class WhiteChecker : Checker
    {
        public WhiteChecker(Cell cell, PlayerMoveDirection playerMoveDirection, CheckerGraphicalImplementation checkerGraphical) : base(cell, playerMoveDirection, checkerGraphical)
        {
            Draw(Color.White, cell);
        }
    }
}