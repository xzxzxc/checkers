using CheckersLibrary.Cells;
using CheckersLibrary.GraphicalImplementation;
using CheckersLibrary.Moves;

namespace CheckersLibrary.Checkers
{
    public class BlackChecker : Checker
    {
        public BlackChecker(Cell cell, MoveDirection moveDirection, CheckerGraphicalImplementation checkerGraphical) : base(cell, moveDirection, checkerGraphical)
        {
            Draw(Color.Black, cell);
        }
    }
}