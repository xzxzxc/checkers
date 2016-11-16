using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace checkers
{
    /// <summary>
    /// class for controling checkers
    /// </summary>
    public abstract class Checker
    {
        public bool isQueen = false; // true if queen and false if simple
        public bool isSelected = false; // true if selected and false if not
        public PictureBox chBox; // image of checker for windows form
        public Cell myCell; // cell on table witch contains this checker

        /// <summary>
        /// move checker, and do all what is needed while moving (kill other checkers, computeallowed etc.)
        /// </summary>
        /// <param name="move">corresponding move</param>
        public void Move(Move move)
        {
            if (this is WhiteChecker)
            {
                myCell.checker = null;
                foreach (Checker ch in move.killedChs)
                {
                    ch.myCell.checker = null;
                    ch.chBox.Visible = false;
                    GameDataHandler.chsBlack.Remove(ch as BlackChecker);
                }
                move.toCell.checker = this;
                if (move.toCell.GetNumber()[1] == 0)
                {
                    isQueen = true;
                    chBox.Image = global::checkers.Properties.Resources.white_checker_queen;
                }
            }
            else
            {
                myCell.checker = null;
                foreach (Checker ch in move.killedChs)
                {
                    ch.myCell.checker = null;
                    ch.chBox.Visible = false;
                    GameDataHandler.chsWhite.Remove(ch as WhiteChecker);
                }
                move.toCell.checker = this;
                if (move.toCell.GetNumber()[1] == 7)
                {
                    isQueen = true;
                    chBox.Image = global::checkers.Properties.Resources.black_checker_queen;
                }
            }

            // end of game
            if (GameDataHandler.chsBlack.Count == 0)
                MessageBox.Show("White win");
            if (GameDataHandler.chsWhite.Count == 0)
                MessageBox.Show("Black win");

            myCell = move.toCell;
            chBox.Location = new System.Drawing.Point(move.toCell.GetLocation()[0], move.toCell.GetLocation()[1]);

            GameDataHandler.allowedMoves = new List<Move>();
            if (this is WhiteChecker)
                foreach (Checker ch in GameDataHandler.chsBlack)
                    AllowedMovesCalculator.Calculate(ch);
            else
                foreach (Checker ch in GameDataHandler.chsWhite)
                    AllowedMovesCalculator.Calculate(ch);
            #region rule: must kill, not go
            foreach (Move mv in GameDataHandler.allowedMoves)
                if (mv.killedChs.Count > 0)
                {
                    for (int i = GameDataHandler.allowedMoves.Count - 1; i >= 0; i--)
                        if (GameDataHandler.allowedMoves[i].killedChs.Count == 0 || GameDataHandler.allowedMoves[i].killedChs==null)
                            GameDataHandler.allowedMoves.RemoveAt(i);
                    break;
                }

            #endregion
            GameDataHandler.selected.unselect();
            Game.isWhite = !Game.isWhite;
        }
        /// <summary>
        /// method for selecting checker
        /// </summary>
        private void select()
        {
            chBox.BackColor = System.Drawing.SystemColors.ControlDark;
            isSelected = true;
            GameDataHandler.selected = this;
        }
        /// <summary>
        /// method for unselecting checker
        /// </summary>
        public void unselect()
        {
            chBox.BackColor = System.Drawing.Color.Transparent;
            isSelected = false;
            GameDataHandler.selected = null;
        }
        /// <summary>
        /// method that calls, when checker was pressed (call select method for corresponding checker)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chBox_Click(Object sender, EventArgs e)
        {
            if (Game.isWhite == this is WhiteChecker)
            {
                if(GameDataHandler.selected!=null)
                    GameDataHandler.selected.unselect();
                select();
            }

        }
        /// <summary>
        /// Constructor of checker
        /// </summary>
        /// <param name="myCell">cell, where the checker located</param>
        /// <param name="color">black, or white</param>
        public Checker(Cell myCell)
        {
            this.myCell = myCell;
            int[] location = myCell.GetLocation();
            chBox = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(chBox)).BeginInit();

            chBox.BackColor = System.Drawing.Color.Transparent;
            chBox.Location = new System.Drawing.Point(location[0], location[1]);
            chBox.Name = "chBox";
            chBox.Size = new System.Drawing.Size(75, 75);
            chBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            chBox.TabIndex = 1;
            chBox.TabStop = false;
            chBox.Click += new System.EventHandler(chBox_Click);

            ((System.ComponentModel.ISupportInitialize)(chBox)).EndInit();
        }
        /// <summary>
        /// Constructor of virtual (nephew) checker, it's using while computing allowed moves, for multiple-kill moves
        /// </summary>
        /// <param name="cell">Cell, where nephew located</param>
        public Checker(Checker ch, Cell cell)
        {
            myCell = cell;
            isQueen = ch.isQueen;
        }
    }
    
    public class WhiteChecker : Checker
    {
        public WhiteChecker(Cell cell) : base(cell)
        {
            chBox.Image = global::checkers.Properties.Resources.white_checker;
        }
        public WhiteChecker(Checker ch, Cell cell):base(ch, cell){ }
    }
    public class BlackChecker : Checker
    {
        public BlackChecker(Cell cell) : base(cell)
        {
            chBox.Image = global::checkers.Properties.Resources.black_checker;
        }
        public BlackChecker(Checker ch, Cell cell) : base(ch, cell) { }
    }
}