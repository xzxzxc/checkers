using CheckersLibrary.Cells;
using CheckersLibrary.Checkers;

namespace CheckersLibrary.AllowedMovesCalculator
{
    public struct Node
    {
        public Cell Cell { get; }
        public Checker Killed { get; }
        public MoveDirection PreviousMoveDirection { get; }

        public Node(Cell cell, Checker killed, MoveDirection previousMoveDirection)
        {
            Cell = cell;
            Killed = killed;
            PreviousMoveDirection = previousMoveDirection;
        }
    }
}