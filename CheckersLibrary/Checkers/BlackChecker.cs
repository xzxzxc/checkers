using CheckersLibrary.Cells;
using CheckersLibrary.GraphicalImplementation;
using CheckersLibrary.Moves;

namespace CheckersLibrary.Checkers
{
    public class BlackChecker : Checker
    {
        public BlackChecker(Cell cell, PlayerMoveDirection playerMoveDirection, CheckerGraphicalImplementation checkerGraphical) : base(cell, playerMoveDirection, checkerGraphical)
        {
            Draw(Color.Black, cell);
        }
    }
}