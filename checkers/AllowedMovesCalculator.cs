using System;
using System.Collections.Generic;
using checkers.Cells;
using checkers.Checkers;
using checkers.Moves;

namespace checkers
{
    public static class AllowedMovesCalculator
    {
        private static Checker _checker;
        private static Cell _primaryCell;
        private static Move _previousMove;
        private static List<Checker> _killedList;
        private static bool _inLoop;

        private static void CheckMovesWithKill()
        {
            Checker killed;
            if (!(_previousMove is MoveBackwardRight) && _checker.CheckKillForwardLeft(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveForwardLeft(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillForwardLeft(killed))
                {
                    _inLoop = true;
                    _checker.Move(toCell);
                    CheckMovesWithKill();
                }
                EndOfMultipleMove();
            }
            if (!(_previousMove is MoveBackwardLeft) && _checker.CheckKillForwardRight(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveForwardRight(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillForwardRight(killed))
                {
                    _inLoop = true;
                    _checker.Move(toCell);
                    CheckMovesWithKill();
                }
                EndOfMultipleMove();
            }
            if (!(_previousMove is MoveForwardRight) && _checker.CheckKillBackwardLeft(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveBackwardLeft(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillBackwardLeft(killed))
                {
                    _inLoop = true;
                    _checker.Move(toCell);
                    CheckMovesWithKill();
                }
                EndOfMultipleMove();
            }
            if (!(_previousMove is MoveForwardLeft) && _checker.CheckKillBackwardRight(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveBackwardRight(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillBackwardRight(killed))
                {
                    _inLoop = true;
                    _checker.Move(toCell);
                    CheckMovesWithKill();
                }
                EndOfMultipleMove();
            }
            // EndOfMultipleMove();
        }

        private static void EndOfMultipleMove()
        {
            _killedList = new List<Checker>();
            _previousMove = null;
            _checker.Cell = _primaryCell;
        }

        /// <summary>
        /// Checking allowed moves and appending them to corresponding list in GameDataHandler
        /// </summary>
        public static void Calculate(Checker checker)
        {
            RememberChecker(checker);
            Initialize();
            ClearPreviousAllowedMoves();
            CheckMovesWithoutKill();
            CheckMovesWithKill();
            EndOfMultipleMove();
        }

        private static void ClearPreviousAllowedMoves()
        {
            _checker.ClearMoves();
        }
        private static void CheckMovesWithoutKill()
        {
            if (_checker.CheckForwardLeft())
                _checker.AddMoveForwardLeft();
            if (_checker.CheckForwardRight())
                _checker.AddMoveForwardRight();
            if (_checker.CheckBackwardLeft())
                _checker.AddMoveBackwardLeft();
            if (_checker.CheckBackwardRight())
                _checker.AddMoveBackwardRight();
        }

        private static void RememberChecker(Checker checker)
        {
            _checker = checker;
        }

        private static void Initialize()
        {
            _primaryCell = _checker.Cell;
            _killedList = new List<Checker>();
            _previousMove = null;
        }
    }
}