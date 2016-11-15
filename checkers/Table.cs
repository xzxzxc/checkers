using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace checkers
{
    /// <summary>
    /// Class for controling game table
    /// </summary>
    public static class Table
    {
        public static PictureBox tableBox; // table pic
        public static Cell[,] cells = new Cell[8, 8]; // list of cells on tanle
        public static Checker selected = null; // current selected checker
        /// <summary>
        /// Method for deleting checker from table
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void delChecker(int x, int y)
        {
            Cell cell = cells[x, y];
            for (int i = Game.chsBlack.Count - 1; i >= 0; --i)
                if (Game.chsBlack[i].myCell == cell)
                {
                    Game.chsBlack[i].chBox.Visible = false;
                    Game.chsBlack.RemoveAt(i);
                    return;
                }
            for (int i = Game.chsWhite.Count - 1; i >=0; --i)
                if (Game.chsWhite[i].myCell == cell)
                {
                    Game.chsWhite[i].chBox.Visible = false;
                    Game.chsWhite.RemoveAt(i);
                    return;
                }
        }
        /// <summary>
        /// Method that calls when table was clicked (call move method for selected checker if available)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static private void tableBox_Click(Object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            int X = me.X / 75;
            int Y = me.Y / 75;
            Cell clickCell = cells[X, Y];
            if (selected == null)
                return;
            foreach (Move m in Game.allowedMoves)
                if (m.toCell == clickCell && m.getSuperUncle() == selected)
                {
                    selected.Move(m);
                    selected = null;
                    break;
                }
        }
        /// <summary>
        /// Create table
        /// </summary>
        public static void CreateTable()
        {
            tableBox = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(tableBox)).BeginInit();
            // 
            // tableBox
            // 
            tableBox.Image = global::checkers.Properties.Resources.table;
            tableBox.Location = new System.Drawing.Point(0, 0);
            tableBox.Name = "tableBox";
            tableBox.Size = new System.Drawing.Size(600, 600);
            tableBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            tableBox.TabIndex = 0;
            tableBox.TabStop = false;
            tableBox.Click += new System.EventHandler(tableBox_Click);

            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    cells[i, j] = new Cell(i, j);
        }
        /// <summary>
        /// Get checker that located on cell
        /// </summary>
        /// <param name="cell">cell</param>
        /// <returns>checker</returns>
        public static Checker getChecker(Cell cell)
        {
            foreach (Checker ch in Game.chsBlack)
                if (ch.myCell == cell)
                    return ch;
            foreach (Checker ch in Game.chsWhite)
                if (ch.myCell == cell)
                    return ch;
            MessageBox.Show("no checker");
            return null;
        }
    }
}