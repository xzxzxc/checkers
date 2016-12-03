using System;
using System.Collections.Generic;
using System.Drawing;
using checkers.Cells;
using checkers.GraphicalImplementation;
using checkers.Moves;

namespace checkers.Checkers
{
    /// <summary>
    /// class for controling checkers
    /// </summary>
    public abstract class Checker
    {
        private CheckerGraphicalImplementation _graphicalImpl;
        public event Click Click;
        /// <summary>
        /// cell on table witch contains this checker
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
        /// list of alowed moves for all checkers
        /// </summary>
        public List<Move> allowedMoves
        {
            private set { _impl.AllowedMoves = value; }
            get { return _impl.AllowedMoves; }
        }

        public MoveDirection MoveDir => _impl.MoveDir;

        private CheckerImpl _impl;

        public void Kill()
        {
            Cell.Checker = null;
            _graphicalImpl.Visible = false;
            if (this is WhiteChecker)
                GameDataHandler.chsWhite.Remove(this as WhiteChecker);
            else
                GameDataHandler.chsBlack.Remove(this as BlackChecker);
        }

        public void ClearMoves()
        {
            allowedMoves = new List<Move>();
        }

        public Object GetImage()
        {
            return _graphicalImpl.GetImage();
        }

        /// <summary>
        /// method for selecting checker
        /// </summary>
        private void select()
        {
            _graphicalImpl.ChancheBgColor(Color.Khaki);
            foreach (Move move in allowedMoves)
            {
                move.toCell.ChangeBgColor(Color.BlueViolet);
                move.toCell.Click += move.Do;
            }
            GameDataHandler.selected = this;
        }
        /// <summary>
        /// method for unselecting checker
        /// </summary>
        public void Unselect()
        {
            _graphicalImpl.ChancheBgColor(Color.Transparent);
            foreach (Move move in allowedMoves)
            {
                move.toCell.ChangeBgColor(Color.Gray);
                move.toCell.Click -= move.Do;
            }
            GameDataHandler.selected = null;
        }

        /// <summary>
        /// method that calls, when checker was pressed (call select method for corresponding checker)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckerClick()
        {
            if (Game.isWhite == this is WhiteChecker)
            {
                if(GameDataHandler.selected!=null)
                    GameDataHandler.selected.Unselect();
                select();
            }
        }
        /// <summary>
        /// Constructor of checker
        /// </summary>
        /// <param name="cell">cell, where the checker located</param>
        /// <param name="color">black, or white</param>
        protected Checker(Cell cell, MoveDirection moveDirection)
        {
            _graphicalImpl = new WhinowsCheckerImplementation(cell);
            _graphicalImpl.CheckerClick += CheckerClick;
            _graphicalImpl.CheckerClick += () => Click?.Invoke();
            _impl = new SimpleChImpl(moveDirection);
            _impl.Cell = cell;
            cell.Checker = this;
        }

        public virtual void BeQueen()
        {
            _impl = new QueenChImpl(MoveDir) {Cell = _impl.Cell };
            _graphicalImpl.BeQueen();
        }

        protected void Draw(Color color)
        {
            _graphicalImpl.CheckerDraw(color);
        }

        public bool CheckForwardLeft()
        {
            return _impl.CheckForwardLeft();
        }

        public  bool CheckForwardRight()
        {
            return _impl.CheckForwardRight();
        }

        public  bool CheckBackwardLeft()
        {
            return _impl.CheckBackwardLeft();
        }

        public  bool CheckBackwardRight()
        {
            return _impl.CheckBackwardRight();
        }

        public  bool CheckKillForwardLeft(out Checker killed)
        {
            return _impl.CheckKillForwardLeft(out killed);
        }

        public  bool CheckKillForwardRight(out Checker killed)
        {
            return _impl.CheckKillForwardRight(out killed);
        }

        public  bool CheckKillBackwardLeft(out Checker killed)
        {
            return _impl.CheckKillBackwardLeft(out killed);
        }

        public  bool CheckKillBackwardRight(out Checker killed)
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

        public void Move(Cell toCell)
        {
            Cell = toCell;
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