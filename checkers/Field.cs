using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace checkers
{
    /// <summary>
    /// Class for controling game table
    /// </summary>
    public static class Field
    {
        public static PictureBox tableBox; // table pic

        /// <summary>
        /// Method for deleting checker from table
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void delChecker(int x, int y)
        {
            Cell cell = TableCells.Cell[x, y];
            for (int i = GameDataHandler.chsBlack.Count - 1; i >= 0; --i)
                if (GameDataHandler.chsBlack[i].myCell == cell)
                {
                    GameDataHandler.chsBlack[i].chBox.Visible = false;
                    GameDataHandler.chsBlack.RemoveAt(i);
                    return;
                }
            for (int i = GameDataHandler.chsWhite.Count - 1; i >=0; --i)
                if (GameDataHandler.chsWhite[i].myCell == cell)
                {
                    GameDataHandler.chsWhite[i].chBox.Visible = false;
                    GameDataHandler.chsWhite.RemoveAt(i);
                    return;
                }
        }
        /// <summary>
        /// Method that calls when table was clicked (call move method for selected checker if available)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void tableBox_Click(Object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            int X = me.X / 75;
            int Y = me.Y / 75;
            Cell clickCell = TableCells.Cell[X, Y];
            if (GameDataHandler.selected == null)
                return;
            foreach (Move m in GameDataHandler.allowedMoves)
                if (m.toCell == clickCell && m.Checker == GameDataHandler.selected)
                {
                    GameDataHandler.selected.Move(m);
                    GameDataHandler.selected = null;
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
        }
    }
}