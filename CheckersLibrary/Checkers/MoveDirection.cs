namespace CheckersLibrary.Checkers
{
    public enum MoveDirection
    {
        ForwardLeft=1,
        ForwardRight=2,
        BackwardLeft=~ForwardRight,
        BackwardRight=~ForwardLeft
    }
}