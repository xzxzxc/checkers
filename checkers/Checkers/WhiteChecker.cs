using System.Drawing;
using checkers.Cells;
using checkers.Moves;

namespace checkers.Checkers
{
    public class WhiteChecker : Checker
    {
        public WhiteChecker(Cell cell, MoveDirection moveDirection) : base(cell, moveDirection)
        {
            Draw(Color.White);
        }
    }
}