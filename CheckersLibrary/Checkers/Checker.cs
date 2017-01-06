using System;
using System.Collections.Generic;
using CheckersLibrary.Cells;
using CheckersLibrary.Checkers.Implementations;
using CheckersLibrary.GraphicalImplementation;
using CheckersLibrary.Moves;

namespace CheckersLibrary.Checkers
{
    /// <summary>
    /// Abstartct class for controling checkers
    /// </summary>
    public abstract class Checker
    {
        /// <summary>
        /// Checker graphical implemenatation
        /// </summary>
        private CheckerGraphicalImplementation CheckerGraphicalImplementation { get; }

        /// <summary>
        /// Standart click event
        /// </summary>
        public event Click Click;

        /// <summary>
        /// Cell on table witch contains this checker
        /// </summary>
        public Cell Cell
        {
            set
            {
                CheckerImpl.Cell?.Clear();
                CheckerImpl.Cell = value;
                CheckerImpl.Cell.Checker = this;
            }
            get { return CheckerImpl.Cell; }
        }

        /// <summary>
        /// List of alowed moves for this checker
        /// </summary>
        public List<Move> AllowedMoves
        {
            private set { CheckerImpl.AllowedMoves = value; }
            get { return CheckerImpl.AllowedMoves; }
        }

        /// <summary>
        /// Direction of moves
        /// </summary>
        public PlayerMoveDirection PlayerMoveDirection => CheckerImpl.PlayerMoveDir;

        /// <summary>
        /// Checker nature implementation
        /// </summary>
        //private CheckerImpl CheckerImpl;

        /// <summary>
        /// Checker nature implementation
        /// </summary>
        private CheckerImpl CheckerImpl { get; set; }

        /// <summary>
        /// Make checker ded
        /// </summary>
        public void Kill()
        {
            Cell.Clear();
            CheckerGraphicalImplementation.Visible = false;
            if (this is WhiteChecker)
                GameDataHandler.WhiteCheckers.Remove(this as WhiteChecker);
            else
                GameDataHandler.BlackCheckers.Remove(this as BlackChecker);
        }
        /// <summary>
        /// Make checker alive
        /// </summary>
        public void Resurect()
        {
            Cell.Checker = this;
            CheckerGraphicalImplementation.Visible = true;
            if (this is WhiteChecker)
                GameDataHandler.WhiteCheckers.Add(this as WhiteChecker);
            else
                GameDataHandler.BlackCheckers.Add(this as BlackChecker);
        }
        /// <summary>
        /// Clear allowed moves
        /// </summary>
        public void ClearMoves()
        {
            AllowedMoves = new List<Move>();
        }
        /// <summary>
        /// Get checker image
        /// </summary>
        /// <returns></returns>
        public Object GetImage()
        {
            return CheckerGraphicalImplementation.GetImage();
        }

        /// <summary>
        /// Make checker selected
        /// </summary>
        private void Select()
        {
            CheckerGraphicalImplementation.ChancheBgColor(Color.Khaki);
            foreach (Move move in AllowedMoves)
            {
                move.ToCell.ChangeBgColor(Color.BlueViolet);
                move.ToCell.Click += move.Do;
                move.ToCell.Click += move.AddSelfToGameMoveList;
            }
            GameDataHandler.Selected = this;
        }
        /// <summary>
        /// Make checker unselected
        /// </summary>
        public void Unselect()
        {
            CheckerGraphicalImplementation.ChancheBgColor(Color.Transparent);
            foreach (Move move in AllowedMoves)
            {
                move.ToCell.ChangeBgColor(Color.Gray);
                move.ToCell.Click -= move.Do;
                move.ToCell.Click -= move.AddSelfToGameMoveList;
            }
            GameDataHandler.Selected = null;
        }

        /// <summary>
        /// Calls, when checker was pressed
        /// </summary>
        private void CheckerClick()
        {
            if (Game.IsWhite == this is WhiteChecker)
            {
                if(GameDataHandler.Selected!=null)
                    GameDataHandler.Selected.Unselect();
                Select();
            }
        }

        /// <summary>
        /// Constructor of checker
        /// </summary>
        /// <param name="cell">Cell, where the checker located</param>
        /// <param name="playerMoveDirection">Direction of moves</param>
        /// <param name="graphicalImplementation">Graphical implementation of checker</param>
        protected Checker(Cell cell, PlayerMoveDirection playerMoveDirection, CheckerGraphicalImplementation graphicalImplementation)
        {
            if (graphicalImplementation != null)
            {
                CheckerGraphicalImplementation = graphicalImplementation;
                CheckerGraphicalImplementation.SpriteClick += CheckerClick;
                CheckerGraphicalImplementation.SpriteClick += () => Click?.Invoke();
            }
            CheckerImpl = new SimpleChImpl(playerMoveDirection);
            Cell = cell;
            // cell.Checker = this;
        }
        /// <summary>
        /// Start be queen
        /// </summary>
        public void BeQueen()
        {
            CheckerImpl = new QueenChImpl(PlayerMoveDirection) {Cell = CheckerImpl.Cell };
            CheckerGraphicalImplementation?.BeQueen();
        }
        /// <summary>
        /// Stop be queen
        /// </summary>
        public void UnBeQueen()
        {
            CheckerImpl = new SimpleChImpl(PlayerMoveDirection) { Cell = CheckerImpl.Cell };
            CheckerGraphicalImplementation.UnBeQueen();
        }

        /// <summary>
        /// Draw checker on field
        /// </summary>
        /// <param name="color"></param>
        /// <param name="cell"></param>
        protected void Draw(Color color, Cell cell)
        {
            CheckerGraphicalImplementation?.Draw(color, cell);
        }
        /// <summary>
        /// Move checker to cell
        /// </summary>
        /// <param name="toCell">Where checker will be moved</param>
        public void Move(Cell toCell)
        {
            Cell = toCell;
        }

        /// <summary>
        /// Check if move without kills is allowed
        /// </summary>
        /// <param name="moveDirection">Move direction</param>
        /// <returns>True, if allowed, false if not</returns>
        public bool Check(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.ForwardLeft:
                    return CheckerImpl.CheckForwardLeft();
                case MoveDirection.ForwardRight:
                    return CheckerImpl.CheckForwardRight();
                case MoveDirection.BackwardLeft:
                    return CheckerImpl.CheckBackwardLeft();
                case MoveDirection.BackwardRight:
                    return CheckerImpl.CheckBackwardRight();
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }

        /// <summary>
        /// Check if move with kill is allowed
        /// </summary>
        /// <param name="killed">Killed checker</param>
        /// <param name="moveDirection">Move direction</param>
        /// <returns>True, if allowed, false if not</returns>
        public bool CheckKill(MoveDirection moveDirection, out Checker killed)
        {
            switch (moveDirection)
            {
                case MoveDirection.ForwardLeft:
                    return CheckerImpl.CheckKillForwardLeft(out killed);
                case MoveDirection.ForwardRight:
                    return CheckerImpl.CheckKillForwardRight(out killed);
                case MoveDirection.BackwardLeft:
                    return CheckerImpl.CheckKillBackwardLeft(out killed);
                case MoveDirection.BackwardRight:
                    return CheckerImpl.CheckKillBackwardRight(out killed);
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }
        /// <summary>
        /// Get cells that can contains checker after kill
        /// </summary>
        /// <param name="moveDirection">Move direction</param>
        /// <param name="killed">Killed checker</param>
        /// <returns>Cells that can contains checker after kill</returns>
        public Cell[] GetCellAfterKill(MoveDirection moveDirection, Checker killed)
        {
            switch (moveDirection)
            {
                case MoveDirection.ForwardLeft:
                    return CheckerImpl.GetCellAfterKillForwardLeft(killed);
                case MoveDirection.ForwardRight:
                    return CheckerImpl.GetCellAfterKillForwardRight(killed);
                case MoveDirection.BackwardLeft:
                    return CheckerImpl.GetCellAfterKillBackwardLeft(killed);
                case MoveDirection.BackwardRight:
                    return CheckerImpl.GetCellAfterKillBackwardRight(killed);
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }

        /// <summary>
        /// Add move without kill
        /// </summary>
        /// <param name="moveDirection">Move direction</param>
        public void AddMove(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.ForwardLeft:
                    CheckerImpl.AddMoveForwardLeft(this);
                    return;
                case MoveDirection.ForwardRight:
                    CheckerImpl.AddMoveForwardRight(this);
                    return;
                case MoveDirection.BackwardLeft:
                    CheckerImpl.AddMoveBackwardLeft(this);
                    return;
                case MoveDirection.BackwardRight:
                    CheckerImpl.AddMoveBackwardRight(this);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }
        /// <summary>
        /// Add move with kills
        /// </summary>
        /// <param name="moveDirection">Move direction</param>
        /// <param name="killed">Checkers killed while move</param>
        /// <param name="move">Move that was added</param>
        public void AddMove(MoveDirection moveDirection, Checker[] killed, out Move move)
        {
            switch (moveDirection)
            {
                case MoveDirection.ForwardLeft:
                    CheckerImpl.AddMoveForwardLeft(this, killed, out move);
                    return;
                case MoveDirection.ForwardRight:
                    CheckerImpl.AddMoveForwardRight(this, killed, out move);
                    return;
                case MoveDirection.BackwardLeft:
                    CheckerImpl.AddMoveBackwardLeft(this, killed, out move);
                    return;
                case MoveDirection.BackwardRight:
                    CheckerImpl.AddMoveBackwardRight(this, killed, out move);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }
    }
}