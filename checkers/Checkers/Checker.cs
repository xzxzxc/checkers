using System;
using System.Collections.Generic;
using System.Drawing;
using checkers.Cells;
using checkers.Checkers.Implementations;
using checkers.GraphicalImplementation;
using checkers.Moves;

namespace checkers.Checkers
{
    /// <summary>
    /// Abstartct class for controling checkers
    /// </summary>
    public abstract class Checker
    {
        /// <summary>
        /// Checker graphical implemenatation
        /// </summary>
        private readonly CheckerGraphicalImplementation _graphicalImpl;
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
                _impl.Cell.Clear();
                _impl.Cell = value;
                _impl.Cell.Checker = this;
            }
            get { return _impl.Cell; }
        }
        /// <summary>
        /// List of alowed moves for this checker
        /// </summary>
        public List<Move> AllowedMoves
        {
            private set { _impl.AllowedMoves = value; }
            get { return _impl.AllowedMoves; }
        }
        /// <summary>
        /// Direction of moves
        /// </summary>
        public MoveDirection MoveDir => _impl.MoveDir;
        /// <summary>
        /// Checker nature implementation
        /// </summary>
        private CheckerImpl _impl;
        /// <summary>
        /// Make checker ded
        /// </summary>
        public void Kill()
        {
            Cell.Clear();
            _graphicalImpl.Visible = false;
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
            _graphicalImpl.Visible = true;
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
            return _graphicalImpl.GetImage();
        }

        /// <summary>
        /// Make checker selected
        /// </summary>
        private void Select()
        {
            _graphicalImpl.ChancheBgColor(Color.Khaki);
            foreach (Move move in AllowedMoves)
            {
                move.ToCell.ChangeBgColor(Color.BlueViolet);
                move.ToCell.Click += move.Do;
            }
            GameDataHandler.Selected = this;
        }
        /// <summary>
        /// Make checker unselected
        /// </summary>
        public void Unselect()
        {
            _graphicalImpl.ChancheBgColor(Color.Transparent);
            foreach (Move move in AllowedMoves)
            {
                move.ToCell.ChangeBgColor(Color.Gray);
                move.ToCell.Click -= move.Do;
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
        /// <param name="moveDirection">Direction of moves</param>
        protected Checker(Cell cell, MoveDirection moveDirection)
        {
            _graphicalImpl = new WhinowsCheckerImplementation(cell);
            _graphicalImpl.SpriteClick += CheckerClick;
            _graphicalImpl.SpriteClick += () => Click?.Invoke();
            _impl = new SimpleChImpl(moveDirection);
            _impl.Cell = cell;
            cell.Checker = this;
        }
        /// <summary>
        /// Start be queen
        /// </summary>
        public void BeQueen()
        {
            _impl = new QueenChImpl(MoveDir) {Cell = _impl.Cell };
            _graphicalImpl.BeQueen();
        }
        /// <summary>
        /// Stop be queen
        /// </summary>
        public void UnBeQueen()
        {
            _impl = new SimpleChImpl(MoveDir) { Cell = _impl.Cell };
            _graphicalImpl.UnBeQueen();
        }
        /// <summary>
        /// Draw checker on field
        /// </summary>
        /// <param name="color"></param>
        protected void Draw(Color color)
        {
            _graphicalImpl.Draw(color);
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
        /// Check that checker can go forward left
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckForwardLeft()
        {
            return _impl.CheckForwardLeft();
        }
        /// <summary>
        /// Check that checker can go forward right
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckForwardRight()
        {
            return _impl.CheckForwardRight();
        }
        /// <summary>
        /// Check that checker can go backward left
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckBackwardLeft()
        {
            return _impl.CheckBackwardLeft();
        }
        /// <summary>
        /// Check that checker can go backward right
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckBackwardRight()
        {
            return _impl.CheckBackwardRight();
        }
        /// <summary>
        /// Check that checker can go forward left and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillForwardLeft(out Checker killed)
        {
            return _impl.CheckKillForwardLeft(out killed);
        }
        /// <summary>
        /// Check that checker can go forward right and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillForwardRight(out Checker killed)
        {
            return _impl.CheckKillForwardRight(out killed);
        }
        /// <summary>
        /// Check that checker can go backward left and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillBackwardLeft(out Checker killed)
        {
            return _impl.CheckKillBackwardLeft(out killed);
        }
        /// <summary>
        /// Check that checker can go backward right and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillBackwardRight(out Checker killed)
        {
            return _impl.CheckKillBackwardRight(out killed);
        }

        public Cell[] GetCellAfterKillForwardLeft(Checker killed)
        {
            return _impl.GetCellAfterKillForwardLeft(killed);
        }

        public Cell[] GetCellAfterKillForwardRight(Checker killed)
        {
            return _impl.GetCellAfterKillForwardRight(killed);
        }

        public Cell[] GetCellAfterKillBackwardLeft(Checker killed)
        {
            return _impl.GetCellAfterKillBackwardLeft(killed);
        }

        public Cell[] GetCellAfterKillBackwardRight(Checker killed)
        {
            return _impl.GetCellAfterKillBackwardRight(killed);
        }

        public void AddMoveForwardLeft()
        {
            _impl.AddMoveForwardLeft(this);
        }

        public  void AddMoveForwardRight()
        {
            _impl.AddMoveForwardRight(this);
        }

        public  void AddMoveBackwardLeft()
        {
            _impl.AddMoveBackwardLeft(this);
        }

        public  void AddMoveBackwardRight()
        {
            _impl.AddMoveBackwardRight(this);
        }

        public  void AddMoveForwardLeft(List<Checker> killed, out Move move)
        {
            _impl.AddMoveForwardLeft(this, killed, out move);
        }

        public  void AddMoveForwardRight(List<Checker> killed, out Move move)
        {
            _impl.AddMoveForwardRight(this, killed, out move);
        }

        public  void AddMoveBackwardLeft(List<Checker> killed, out Move move)
        {
            _impl.AddMoveBackwardLeft(this, killed, out move);
        }

        public  void AddMoveBackwardRight(List<Checker> killed, out Move move)
        {
            _impl.AddMoveBackwardRight(this, killed, out move);
        }
    }
}