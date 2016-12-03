using System.Drawing;
using checkers.Cells;
using checkers.Moves;

namespace checkers.Checkers
{
    public class BlackChecker : Checker
    {
        public BlackChecker(Cell cell, MoveDirection moveDirection) : base(cell, moveDirection)
        {
            Draw(Color.Black);
        }
    }
}