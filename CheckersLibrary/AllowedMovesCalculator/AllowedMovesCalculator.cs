using System.Collections.Generic;
using CheckersLibrary.Cells;
using CheckersLibrary.Checkers;
using CheckersLibrary.Moves;

namespace CheckersLibrary.AllowedMovesCalculator
{
    public static class AllowedMovesCalculator
    {
        /// <summary>
        /// current checker
        /// </summary>
        private static Checker _checker;
        /// <summary>
        /// cell of checker at start of computing
        /// </summary>
        private static Cell _primaryCell;
        /// <summary>
        /// checker previous move while computing
        /// </summary>
        private static Move _previousMove;
        /// <summary>
        /// list of killed chackers while computing multiple move
        /// </summary>
        private static List<Checker> _killedList;


        /// <summary>
        /// Checking allowed moves and appending them to corresponding list in GameDataHandler
        /// </summary>
        public static void Calculate(Checker checker)
        {
            RememberChecker(checker);
            Initialize();
            ClearPreviousAllowedMoves();
            ComputeMovesWithoutKill();
            ComputeMovesWithKill();
            EndOfMultipleMove();
        }
        /// <summary>
        /// Compute moves with cills
        /// </summary>
        private static void ComputeMovesWithKill()
        {
            Checker killed;
            if (!(_previousMove is MoveBackwardRight) && _checker.CheckKillForwardLeft(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveForwardLeft(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillForwardLeft(killed))
                {
                    _checker.Move(toCell);
                    ComputeMovesWithKill();
                }
                EndOfMultipleMove();
            }
            if (!(_previousMove is MoveBackwardLeft) && _checker.CheckKillForwardRight(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveForwardRight(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillForwardRight(killed))
                {
                    _checker.Move(toCell);
                    ComputeMovesWithKill();
                }
                EndOfMultipleMove();
            }
            if (!(_previousMove is MoveForwardRight) && _checker.CheckKillBackwardLeft(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveBackwardLeft(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillBackwardLeft(killed))
                {
                    _checker.Move(toCell);
                    ComputeMovesWithKill();
                }
                EndOfMultipleMove();
            }
            if (!(_previousMove is MoveForwardLeft) && _checker.CheckKillBackwardRight(out killed))
            {
                _killedList.Add(killed);
                _checker.AddMoveBackwardRight(_killedList, out _previousMove);
                foreach (Cell toCell in _checker.GetCellAfterKillBackwardRight(killed))
                {
                    _checker.Move(toCell);
                    ComputeMovesWithKill();
                }
                EndOfMultipleMove();
            }
        }
        /// <summary>
        /// Undo changes and cleare variables
        /// </summary>
        private static void EndOfMultipleMove()
        {
            _killedList = new List<Checker>();
            _previousMove = null;
            _checker.Cell = _primaryCell;
        }
        /// <summary>
        /// Delete previous allowed moves
        /// </summary>
        private static void ClearPreviousAllowedMoves()
        {
            _checker.ClearMoves();
        }
        /// <summary>
        /// Compute moves without kills
        /// </summary>
        private static void ComputeMovesWithoutKill()
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
        /// <summary>
        /// Remember current checker
        /// </summary>
        /// <param name="checker"></param>
        private static void RememberChecker(Checker checker)
        {
            _checker = checker;
        }
        /// <summary>
        /// Initialize variables
        /// </summary>
        private static void Initialize()
        {
            _primaryCell = _checker.Cell;
            _killedList = new List<Checker>();
            _previousMove = null;
        }
    }
}