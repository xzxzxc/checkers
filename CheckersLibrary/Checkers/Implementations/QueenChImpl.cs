using System;
using System.Collections.Generic;
using System.Linq;
using CheckersLibrary.Cells;
using CheckersLibrary.Moves;

namespace CheckersLibrary.Checkers.Implementations
{
    public class QueenChImpl : CheckerImpl
    {

        public QueenChImpl(MoveDirection moveDir) : base(moveDir) { }

        public override bool CheckForwardLeft()
        {
            if (Cell.X == 0 || Cell.Y == 0)
                return false;
            if (!TableCells.Cell[Cell.X - 1,Cell.Y - 1].IsClear())
                return false;
            return true;
        }

        public override bool CheckForwardRight()
        {
            if (Cell.X == 7 || Cell.Y == 0)
                return false;
            if (!TableCells.Cell[Cell.X + 1, Cell.Y - 1].IsClear())
                return false;
            return true;
        }

        public override bool CheckBackwardLeft()
        {
            if (Cell.X == 0 || Cell.Y == 7)
                return false;
            if (!TableCells.Cell[Cell.X - 1, Cell.Y + 1].IsClear())
                return false;
            return true;
        }

        public override bool CheckBackwardRight()
        {
            if (Cell.X == 7 || Cell.Y == 7)
                return false;
            if (!TableCells.Cell[Cell.X + 1, Cell.Y + 1].IsClear())
                return false;
            return true;
        }

        public override bool CheckKillForwardLeft(out Checker killed)
        {
            killed = null;
            if (Cell.X == 0 || Cell.Y == 0)
                return false;
            for (int i = Cell.X - 1, j = Cell.Y - 1; i > 0 && j > 0; i--, j--)
            {
                if (!TableCells.Cell[i, j].IsClear())
                {
                    if (TableCells.Cell[i, j].Checker.MoveDirection != MoveDir)
                    {
                        if (TableCells.Cell[i - 1, j - 1].IsClear())
                        {
                            killed = TableCells.Cell[i, j].Checker;
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public override bool CheckKillForwardRight(out Checker killed)
        {
            killed = null;
            if (Cell.X == 7 || Cell.Y == 0)
                return false;
            for (int i = Cell.X + 1, j = Cell.Y - 1; i < 7 && j > 0; i++, j--)
            {
                if (!TableCells.Cell[i, j].IsClear())
                {
                    if (TableCells.Cell[i, j].Checker.MoveDirection != MoveDir)
                    {
                        if (TableCells.Cell[i + 1, j - 1].IsClear())
                        {
                            killed = TableCells.Cell[i, j].Checker;
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public override bool CheckKillBackwardLeft(out Checker killed)
        {
            killed = null;
            if (Cell.X == 0 || Cell.Y == 7)
                return false;
            for (int i = Cell.X - 1, j = Cell.Y + 1; i > 0 && j < 7; i--, j++)
            {
                if (!TableCells.Cell[i, j].IsClear())
                {
                    if (TableCells.Cell[i, j].Checker.MoveDirection != MoveDir)
                    {
                        if (TableCells.Cell[i - 1, j + 1].IsClear())
                        {
                            killed = TableCells.Cell[i, j].Checker;
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public override bool CheckKillBackwardRight(out Checker killed)
        {
            killed = null;
            if (Cell.X == 7 || Cell.Y == 7)
                return false;
            for (int i = Cell.X + 1, j = Cell.Y + 1; i < 7 && j < 7; i++, j++)
            {
                if (!TableCells.Cell[i, j].IsClear())
                {
                    if (TableCells.Cell[i, j].Checker.MoveDirection != MoveDir)
                    {
                        if (TableCells.Cell[i + 1, j + 1].IsClear())
                        {
                            killed = TableCells.Cell[i, j].Checker;
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public override Cell[] GetCellAfterKillForwardLeft(Checker killed)
        {
            Cell[] cells = new Cell[1];
            byte killedX = killed.Cell.X, killedY = killed.Cell.Y;
            cells[0] = TableCells.Cell[killedX - 1, killedY - 1];
            if (killedX > 1 && killedY > 1)
                for (int i = killedX - 2, j = killedY - 2; i >= 0 && j >= 0; i--, j--)
                {
                    if (TableCells.Cell[i, j].IsClear())
                    {
                        Array.Resize(ref cells, cells.Length + 1);
                        cells[cells.Length - 1] = TableCells.Cell[i, j];
                    }
                    else
                        break;
                }
            return cells;
        }

        public override Cell[] GetCellAfterKillForwardRight(Checker killed)
        {
            Cell[] cells = new Cell[1];
            byte killedX = killed.Cell.X, killedY = killed.Cell.Y;
            cells[0] = TableCells.Cell[killedX + 1, killedY - 1];
            if (killedX < 6 && killedY > 1)
                for (int i = killedX + 2, j = killedY - 2; i <= 7 && j >= 0; i++, j--)
                {
                    if (TableCells.Cell[i, j].IsClear())
                    {
                        Array.Resize(ref cells, cells.Length + 1);
                        cells[cells.Length - 1] = TableCells.Cell[i, j];
                    }
                    else
                        break;
                }
            return cells;
        }

        public override Cell[] GetCellAfterKillBackwardLeft(Checker killed)
        {
            Cell[] cells = new Cell[1];
            byte killedX = killed.Cell.X, killedY = killed.Cell.Y;
            cells[0] = TableCells.Cell[killedX - 1, killedY + 1];
            if (killedX > 1 && killedY < 6)
                for (int i = killedX - 2, j = killedY + 2; i >= 0 && j <= 7; i--, j++)
                {
                    if (TableCells.Cell[i, j].IsClear())
                    {
                        Array.Resize(ref cells, cells.Length + 1);
                        cells[cells.Length - 1] = TableCells.Cell[i, j];
                    }
                    else
                        break;
                }
            return cells;
        }

        public override Cell[] GetCellAfterKillBackwardRight(Checker killed)
        {
            Cell[] cells = new Cell[1];
            byte killedX = killed.Cell.X, killedY = killed.Cell.Y;
            cells[0] = TableCells.Cell[killedX + 1, killedY + 1];
            if (killedX < 6 && killedY < 6)
                for (int i = killedX + 2, j = killedY + 2; i <= 7 && j <= 7; i++, j++)
                {
                    if (TableCells.Cell[i, j].IsClear())
                    {
                        Array.Resize(ref cells, cells.Length + 1);
                        cells[cells.Length - 1] = TableCells.Cell[i, j];
                    }
                    else
                        break;
                }
            return cells;
        }

        public override void AddMoveForwardLeft(Checker checker)
        {
            for (int i = Cell.X - 1, j = Cell.Y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (TableCells.Cell[i, j].IsClear())
                    AllowedMoves.Add(new MoveForwardLeft(checker, TableCells.Cell[i, j]));
                else
                    break;
            }
        }

        public override void AddMoveForwardRight(Checker checker)
        {
            for (int i = Cell.X + 1, j = Cell.Y - 1; i < 8 && j >= 0; i++, j--)
            {
                if (TableCells.Cell[i, j].IsClear())
                    AllowedMoves.Add(new MoveForwardRight(checker, TableCells.Cell[i, j]));
                else
                    break;
            }
        }

        public override void AddMoveBackwardLeft(Checker checker)
        {
            for (int i = Cell.X - 1, j = Cell.Y + 1; i >= 0 && j < 8; i--, j++)
            {
                if (TableCells.Cell[i, j].IsClear())
                    AllowedMoves.Add(new MoveBackwardLeft(checker, TableCells.Cell[i, j]));
                else
                    break;
            }
        }

        public override void AddMoveBackwardRight(Checker checker)
        {
            for (int i = Cell.X + 1, j = Cell.Y + 1; i < 8 && j < 8; i++, j++)
            {
                if (TableCells.Cell[i, j].IsClear())
                    AllowedMoves.Add(new MoveBackwardRight(checker, TableCells.Cell[i, j]));
                else
                    break;
            }
        }

        public override void AddMoveForwardLeft(Checker checker, List<Checker> killed, out Move move)
        {
            move = null;
            for (int i = killed.Last().Cell.X - 1, j = killed.Last().Cell.Y - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (TableCells.Cell[i, j].IsClear())
                {
                    move = new MoveForwardLeft(checker, TableCells.Cell[i, j], killed);
                    AllowedMoves.Add(move);
                }
                else
                    break;
            }
        }

        public override void AddMoveForwardRight(Checker checker, List<Checker> killed, out Move move)
        {
            move = null;
            for (int i = killed.Last().Cell.X + 1, j = killed.Last().Cell.Y - 1; i < 8 && j >= 0; i++, j--)
            {
                if (TableCells.Cell[i, j].IsClear())
                {
                    move = new MoveForwardRight(checker, TableCells.Cell[i, j], killed);
                    AllowedMoves.Add(move);
                }
                else
                    break;
            }
        }

        public override void AddMoveBackwardLeft(Checker checker, List<Checker> killed, out Move move)
        {
            move = null;
            for (int i = killed.Last().Cell.X - 1, j = killed.Last().Cell.Y + 1; i >= 0 && j < 8; i--, j++)
            {
                if (TableCells.Cell[i, j].IsClear())
                {
                    move = new MoveBackwardLeft(checker, TableCells.Cell[i, j], killed);
                    AllowedMoves.Add(move);
                }
                else
                    break;
            }
        }

        public override void AddMoveBackwardRight(Checker checker, List<Checker> killed, out Move move)
        {
            move = null;
            for (int i = killed.Last().Cell.X + 1, j = killed.Last().Cell.Y + 1; i < 8 && j < 8; i++, j++)
            {
                if (TableCells.Cell[i, j].IsClear())
                {
                    move = new MoveBackwardRight(checker, TableCells.Cell[i, j], killed);
                    AllowedMoves.Add(move);
                }
                else
                    break;
            }
        }
    }
}