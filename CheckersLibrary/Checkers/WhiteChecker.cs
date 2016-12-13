using CheckersLibrary.Cells;
using CheckersLibrary.GraphicalImplementation;
using CheckersLibrary.Moves;

namespace CheckersLibrary.Checkers
{
    public class WhiteChecker : Checker
    {
        public WhiteChecker(Cell cell, MoveDirection moveDirection, CheckerGraphicalImplementation checkerGraphical) : base(cell, moveDirection, checkerGraphical)
        {
            Draw(Color.White, cell);
        }
    }
}