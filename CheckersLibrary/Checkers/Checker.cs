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
        public MoveDirection MoveDirection => CheckerImpl.MoveDir;

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
        /// <param name="graphicalImplementation">Graphical implementation of checker</param>
        protected Checker(Cell cell, MoveDirection moveDirection, CheckerGraphicalImplementation graphicalImplementation)
        {
            if (graphicalImplementation != null)
            {
                CheckerGraphicalImplementation = graphicalImplementation;
                CheckerGraphicalImplementation.SpriteClick += CheckerClick;
                CheckerGraphicalImplementation.SpriteClick += () => Click?.Invoke();
            }
            CheckerImpl = new SimpleChImpl(moveDirection);
            Cell = cell;
            // cell.Checker = this;
        }
        /// <summary>
        /// Start be queen
        /// </summary>
        public void BeQueen()
        {
            CheckerImpl = new QueenChImpl(MoveDirection) {Cell = CheckerImpl.Cell };
            CheckerGraphicalImplementation?.BeQueen();
        }
        /// <summary>
        /// Stop be queen
        /// </summary>
        public void UnBeQueen()
        {
            CheckerImpl = new SimpleChImpl(MoveDirection) { Cell = CheckerImpl.Cell };
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
        /// Check that checker can go forward left
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckForwardLeft()
        {
            return CheckerImpl.CheckForwardLeft();
        }
        /// <summary>
        /// Check that checker can go forward right
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckForwardRight()
        {
            return CheckerImpl.CheckForwardRight();
        }
        /// <summary>
        /// Check that checker can go backward left
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckBackwardLeft()
        {
            return CheckerImpl.CheckBackwardLeft();
        }
        /// <summary>
        /// Check that checker can go backward right
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckBackwardRight()
        {
            return CheckerImpl.CheckBackwardRight();
        }
        /// <summary>
        /// Check that checker can go forward left and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillForwardLeft(out Checker killed)
        {
            return CheckerImpl.CheckKillForwardLeft(out killed);
        }
        /// <summary>
        /// Check that checker can go forward right and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillForwardRight(out Checker killed)
        {
            return CheckerImpl.CheckKillForwardRight(out killed);
        }
        /// <summary>
        /// Check that checker can go backward left and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillBackwardLeft(out Checker killed)
        {
            return CheckerImpl.CheckKillBackwardLeft(out killed);
        }
        /// <summary>
        /// Check that checker can go backward right and kill
        /// </summary>
        /// <returns>True if can, false if can't</returns>
        public bool CheckKillBackwardRight(out Checker killed)
        {
            return CheckerImpl.CheckKillBackwardRight(out killed);
        }

        public Cell[] GetCellAfterKillForwardLeft(Checker killed)
        {
            return CheckerImpl.GetCellAfterKillForwardLeft(killed);
        }

        public Cell[] GetCellAfterKillForwardRight(Checker killed)
        {
            return CheckerImpl.GetCellAfterKillForwardRight(killed);
        }

        public Cell[] GetCellAfterKillBackwardLeft(Checker killed)
        {
            return CheckerImpl.GetCellAfterKillBackwardLeft(killed);
        }

        public Cell[] GetCellAfterKillBackwardRight(Checker killed)
        {
            return CheckerImpl.GetCellAfterKillBackwardRight(killed);
        }

        public void AddMoveForwardLeft()
        {
            CheckerImpl.AddMoveForwardLeft(this);
        }

        public  void AddMoveForwardRight()
        {
            CheckerImpl.AddMoveForwardRight(this);
        }

        public  void AddMoveBackwardLeft()
        {
            CheckerImpl.AddMoveBackwardLeft(this);
        }

        public  void AddMoveBackwardRight()
        {
            CheckerImpl.AddMoveBackwardRight(this);
        }

        public  void AddMoveForwardLeft(List<Checker> killed, out Move move)
        {
            CheckerImpl.AddMoveForwardLeft(this, killed, out move);
        }

        public  void AddMoveForwardRight(List<Checker> killed, out Move move)
        {
            CheckerImpl.AddMoveForwardRight(this, killed, out move);
        }

        public  void AddMoveBackwardLeft(List<Checker> killed, out Move move)
        {
            CheckerImpl.AddMoveBackwardLeft(this, killed, out move);
        }

        public  void AddMoveBackwardRight(List<Checker> killed, out Move move)
        {
            CheckerImpl.AddMoveBackwardRight(this, killed, out move);
        }
    }
}