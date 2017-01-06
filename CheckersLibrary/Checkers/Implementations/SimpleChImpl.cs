using System;
using System.Collections.Generic;
using System.Threading;
using CheckersLibrary.Cells;
using CheckersLibrary.Moves;


namespace CheckersLibrary.Checkers.Implementations
{
    public class SimpleChImpl : CheckerImpl
    {

        public SimpleChImpl(PlayerMoveDirection playerMoveDir) : base(playerMoveDir) {}

        public override bool CheckForwardLeft()
        {
            if (PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X == 0 || Cell.Y == 0 : Cell.X == 7 || Cell.Y == 7)
                return false;
            if (! TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X - 1 : Cell.X + 1,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1].IsClear())
                return false;
            return true;
        }
        public override bool CheckForwardRight()
        {
            if (PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X == 7 || Cell.Y == 0: Cell.X == 0 || Cell.Y == 7)
                return false;
            if (!TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X + 1 : Cell.X - 1,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1].IsClear())
                return false;
            return true;
        }
        public override bool CheckBackwardLeft()
        {
            return false; // Simple chacker can't go backward without kill
        }
        public override bool CheckBackwardRight()
        {
            return false; // Simple chacker can't go backward without kill
        }
        public override bool CheckKillForwardLeft(out Checker killed)
        {
            killed = null;
            if (PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X < 2 || Cell.Y < 2 : Cell.X > 5 || Cell.Y > 5)
                return false;
            if (!TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X - 1 : Cell.X + 1,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.PlayerMoveDirection == PlayerMoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }
        public override bool CheckKillForwardRight(out Checker killed)
        {
            killed = null;
            if (PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X > 5 || Cell.Y < 2 : Cell.X < 2 || Cell.Y > 5)
                return false;
            if (!TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X + 1 : Cell.X - 1,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.PlayerMoveDirection == PlayerMoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }
        public override bool CheckKillBackwardLeft(out Checker killed)
        {
            killed = null;
            if (PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X < 2 || Cell.Y > 5 : Cell.X > 5 || Cell.Y < 2)
                return false;
            if (!TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X - 1 : Cell.X + 1,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y + 1 : Cell.Y - 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.PlayerMoveDirection == PlayerMoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }
        public override bool CheckKillBackwardRight(out Checker killed)
        {
            killed = null;
            if (PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X > 5 || Cell.Y > 5 : Cell.X < 2 || Cell.Y < 2)
                return false;
            if (!TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X + 1 : Cell.X - 1,
                PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y + 1 : Cell.Y - 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.PlayerMoveDirection == PlayerMoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }

        public override Cell[] GetCellAfterKillForwardLeft(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2]
            };
        }

        public override Cell[] GetCellAfterKillForwardRight(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2]
            };
        }

        public override Cell[] GetCellAfterKillBackwardLeft(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2]
            };
        }

        public override Cell[] GetCellAfterKillBackwardRight(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                    PlayerMoveDir == PlayerMoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2]
            };
        }

        public override void AddMoveForwardLeft(Checker checker)
        {
            AllowedMoves.Add(new Move(checker,
                TableCells.Cell[
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.X - 1 : checker.Cell.X + 1,
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.Y - 1 : checker.Cell.Y + 1]));
        }

        public override void AddMoveForwardRight(Checker checker)
        {
            AllowedMoves.Add(new Move(checker,
                TableCells.Cell[
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.X + 1 : checker.Cell.X - 1,
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.Y - 1 : checker.Cell.Y + 1]));
        }

        public override void AddMoveBackwardLeft(Checker checker)
        {
            throw new Exception("Simple chacker can\'t go backward without kill");
        }

        public override void AddMoveBackwardRight(Checker checker)
        {
            throw new Exception("Simple chacker can\'t go backward without kill");
        }

        public override void AddMoveForwardLeft(Checker checker, Checker[] killed, out Move move)
        {
            move = new Move(checker,
                TableCells.Cell[
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.X - 2 : checker.Cell.X + 2,
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.Y - 2 : checker.Cell.Y + 2],
                killed);
            AllowedMoves.Add(move);
        }

        public override void AddMoveForwardRight(Checker checker, Checker[] killed, out Move move)
        {
            move = new Move(checker,
                TableCells.Cell[
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.X + 2 : checker.Cell.X - 2,
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.Y - 2 : checker.Cell.Y + 2],
                killed);
            AllowedMoves.Add(move);
        }

        public override void AddMoveBackwardLeft(Checker checker, Checker[] killed, out Move move)
        {
            move = new Move(checker,
                TableCells.Cell[
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.X - 2 : checker.Cell.X + 2,
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.Y + 2 : checker.Cell.Y - 2],
                killed);
            AllowedMoves.Add(move);
        }

        public override void AddMoveBackwardRight(Checker checker, Checker[] killed, out Move move)
        {
            move = new Move(checker,
                TableCells.Cell[
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.X + 2 : checker.Cell.X - 2,
                    (checker.PlayerMoveDirection == PlayerMoveDirection.BottomTop) ? checker.Cell.Y + 2 : checker.Cell.Y - 2],
                killed);
            AllowedMoves.Add(move);
        }

    }
}