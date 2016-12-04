using System;
using System.Collections.Generic;
using checkers.Cells;
using checkers.Checkers;
using checkers.Checkers.Implementations;
using checkers.Moves;

namespace checkers
{
    public class SimpleChImpl : CheckerImpl
    {

        public SimpleChImpl(MoveDirection moveDir) : base(moveDir) {}
        private bool _isFirstTime = true;

        public override bool CheckForwardLeft()
        {
            if (MoveDir == MoveDirection.BottomTop ? Cell.X == 0 || Cell.Y == 0 : Cell.X == 7 || Cell.Y == 7)
                return false;
            if (! TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X - 1 : Cell.X + 1,
                MoveDir == MoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1].IsClear())
                return false;
            return true;
        }
        public override bool CheckForwardRight()
        {
            if (MoveDir == MoveDirection.BottomTop ? Cell.X == 7 || Cell.Y == 0: Cell.X == 0 || Cell.Y == 7)
                return false;
            if (!TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X + 1 : Cell.X - 1,
                MoveDir == MoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1].IsClear())
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
            if (MoveDir == MoveDirection.BottomTop ? Cell.X < 2 || Cell.Y < 2 : Cell.X > 5 || Cell.Y > 5)
                return false;
            if (!TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                MoveDir == MoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X - 1 : Cell.X + 1,
                MoveDir == MoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.MoveDir == MoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }
        public override bool CheckKillForwardRight(out Checker killed)
        {
            killed = null;
            if (MoveDir == MoveDirection.BottomTop ? Cell.X > 5 || Cell.Y < 2 : Cell.X < 2 || Cell.Y > 5)
                return false;
            if (!TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                MoveDir == MoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X + 1 : Cell.X - 1,
                MoveDir == MoveDirection.BottomTop ? Cell.Y - 1 : Cell.Y + 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.MoveDir == MoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }
        public override bool CheckKillBackwardLeft(out Checker killed)
        {
            killed = null;
            if (MoveDir == MoveDirection.BottomTop ? Cell.X < 2 || Cell.Y > 5 : Cell.X > 5 || Cell.Y < 2)
                return false;
            if (!TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                MoveDir == MoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X - 1 : Cell.X + 1,
                MoveDir == MoveDirection.BottomTop ? Cell.Y + 1 : Cell.Y - 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.MoveDir == MoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }
        public override bool CheckKillBackwardRight(out Checker killed)
        {
            killed = null;
            if (MoveDir == MoveDirection.BottomTop ? Cell.X > 5 || Cell.Y > 5 : Cell.X < 2 || Cell.Y < 2)
                return false;
            if (!TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                MoveDir == MoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2].IsClear())
                return false;
            Cell nextCell = TableCells.Cell[
                MoveDir == MoveDirection.BottomTop ? Cell.X + 1 : Cell.X - 1,
                MoveDir == MoveDirection.BottomTop ? Cell.Y + 1 : Cell.Y - 1];
            if (nextCell.IsClear())
                return false;
            if (nextCell.Checker.MoveDir == MoveDir)
                return false;
            killed = nextCell.Checker;
            return true;
        }

        public override Cell[] GetCellAfterKillForwardLeft(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    MoveDir == MoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                    MoveDir == MoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2]
            };
        }

        public override Cell[] GetCellAfterKillForwardRight(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    MoveDir == MoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                    MoveDir == MoveDirection.BottomTop ? Cell.Y - 2 : Cell.Y + 2]
            };
        }

        public override Cell[] GetCellAfterKillBackwardLeft(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    MoveDir == MoveDirection.BottomTop ? Cell.X - 2 : Cell.X + 2,
                    MoveDir == MoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2]
            };
        }

        public override Cell[] GetCellAfterKillBackwardRight(Checker killed)
        {
            return new[]
            {
                TableCells.Cell[
                    MoveDir == MoveDirection.BottomTop ? Cell.X + 2 : Cell.X - 2,
                    MoveDir == MoveDirection.BottomTop ? Cell.Y + 2 : Cell.Y - 2]
            };
        }

        public override void AddMoveForwardLeft(Checker checker)
        {
            AllowedMoves.Add(new MoveForwardLeft(checker));
        }

        public override void AddMoveForwardRight(Checker checker)
        {
            AllowedMoves.Add(new MoveForwardRight(checker));
        }

        public override void AddMoveBackwardLeft(Checker checker)
        {
            throw new AccessViolationException("Simple chacker can\'t go backward without kill");
        }

        public override void AddMoveBackwardRight(Checker checker)
        {
            throw new AccessViolationException("Simple chacker can\'t go backward without kill");
        }

        public override void AddMoveForwardLeft(Checker checker, List<Checker> killed, out Move move)
        {
            move = new MoveForwardLeft(checker, killed);
            AllowedMoves.Add(move);
        }

        public override void AddMoveForwardRight(Checker checker, List<Checker> killed, out Move move)
        {
            move = new MoveForwardRight(checker, killed);
            AllowedMoves.Add(move);
        }

        public override void AddMoveBackwardLeft(Checker checker, List<Checker> killed, out Move move)
        {
            move = new MoveBackwardLeft(checker, killed);
            AllowedMoves.Add(move);
        }

        public override void AddMoveBackwardRight(Checker checker, List<Checker> killed, out Move move)
        {
            move = new MoveBackwardRight(checker, killed);
            AllowedMoves.Add(move);
        }

    }
}