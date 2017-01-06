using System;
using System.Collections.Generic;
using System.Linq;
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
        /// checker previous move while computing
        /// </summary>
        private static Move _previousMove;
        /// <summary>
        /// if true, after next move checker will be queen
        /// </summary>
        private static bool _setQueen;
        /// <summary>
        /// List of nodes
        /// </summary>
        private static LinkedList<Node> _nodes;


        /// <summary>
        /// Checking allowed moves and appending them to corresponding list in GameDataHandler
        /// </summary>
        public static void Calculate(Checker checker)
        {
            Initialize(checker);
            ClearPreviousAllowedMoves();
            ComputeMovesWithoutKill();
            ComputeMovesWithKill();
            EndOfMultipleMove();
            _checker = null;
        }
        //private static MoveDirection GetOppositeDirection
        /// <summary>
        /// Compute moves with cills
        /// </summary>
        private static void ComputeMovesWithKill()
        {
            foreach (MoveDirection moveDir in  Enum.GetValues(typeof(MoveDirection)))
            {
                Checker killed;
                if (_checker.CheckKill(moveDir, out killed))
                {
                    if (TryAddNode(killed, moveDir))
                    {
                        var killeds = from node in _nodes
                            select node.Killed;
                        _checker.AddMove(moveDir, killeds.ToArray(), out _previousMove);
                        SetQueenIfNeeded();
                        CheckWillNeedSetQueenNextMoves();
                        foreach (Cell toCell in _checker.GetCellAfterKill(moveDir, killed))
                        {
                            _checker.Move(toCell);
                            ComputeMovesWithKill();
                        }
                        // to prev node
                        EndOfMultipleMove();
                    }
                }
            }
        }

        private static void CheckWillNeedSetQueenNextMoves()
        {
            if (_previousMove.SetQueen)
                _setQueen = true;
        }

        private static void SetQueenIfNeeded()
        {
            if (_setQueen)
                _previousMove.SetQueen = true;
        }

        private static bool TryAddNode(Checker killed, MoveDirection currentMoveDirection)
        {
            if (_nodes.Last?.Value.PreviousMoveDirection == ~currentMoveDirection) return false;
            if (IsDublicate()) return false;
            _nodes.AddLast(new Node(_checker.Cell, killed, currentMoveDirection));
            return true;
        }

        private static bool IsDublicate()
        {
            foreach (var node in _nodes)
                if (_checker.Cell == node.Cell)
                    return true;
            return false;
        }

        /// <summary>
        /// Undo changes and cleare variables
        /// </summary>
        private static void EndOfMultipleMove()
        {
            _previousMove = null;
            _setQueen = false;
            if (_nodes.Last == null) return;
            _checker.Move(_nodes.Last.Value.Cell);
            _nodes.RemoveLast();
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
            foreach (MoveDirection moveDir in Enum.GetValues(typeof(MoveDirection)))
                if (_checker.Check(moveDir))
                    _checker.AddMove(moveDir);
        }
        /// <summary>
        /// Initialize variables
        /// </summary>
        private static void Initialize(Checker checker)
        {
            _checker = checker;
            _nodes = new LinkedList<Node>();
            _setQueen = false;
            _previousMove = null;
        }
    }
}