using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace checkers
{
    /// <summary>
    /// class for controling checkers
    /// </summary>
    public class Checker
    {
        public bool isQueen = false; // true if queen and false if simple
        public bool isSelected = false; // true if selected and false if not
        public PictureBox chBox; // image of checker for windows form
        public Cell myCell; // cell on table witch contains this checker
        public Checker uncle = null; // uncle checker (using when computing allowed moves)

        /// <summary>
        /// computing allowed moves and appending them to corresponding list
        /// </summary>
        public void computeAllowedMoves()
        {
            int x = myCell.GetNumber()[0];
            int y = myCell.GetNumber()[1];
            #region White
            if (this is WhiteChecker)
            {
                #region Queen
                if (isQueen)
                {
                    #region forward left
                    if (x > 0 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x - 1, currY = y - 1; currX >= 0 && currY >= 0; currX--, currY--)
                            {
                                if (Table.cells[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y - 1; ix != currX; ix--, iy--)
                                    {
                                        if (Table.cells[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix + 1, iy + 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                    #endregion
                    #region forward right
                    if (x < 7 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x + 1, currY = y - 1; currX <= 7 && currY >= 0; currX++, currY--)
                            {
                                if (Table.cells[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y - 1; ix != currX; ix++, iy--)
                                    {
                                        if (Table.cells[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix - 1, iy + 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                    #endregion
                    #region backward left
                    if (x > 0 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x - 1, currY = y + 1; currX >= 0 && currY <= 7; currX--, currY++)
                            {
                                if (Table.cells[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y + 1; ix != currX; ix--, iy++)
                                    {
                                        if (Table.cells[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix + 1, iy - 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                    #endregion
                    #region backward right
                    if (x < 7 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x + 1, currY = y + 1; currX <= 7 && currY <= 7; currX++, currY++)
                            {
                                if (Table.cells[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y + 1; ix != currX; ix++, iy++)
                                    {
                                        if (Table.cells[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix - 1, iy - 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                }
                    #endregion
                    #endregion
                    #region Simple
                else
                {
                    if (y > 0)
                    {
                        if (x > 0)
                        {
                            // move left
                            if (Table.cells[x - 1, y - 1].checker==null && uncle == null) // "virtual" checkers can onle beat, not go
                                Game.allowedMoves.Add(new Move(this, Table.cells[x - 1, y - 1]));
                            // move forward left & kill
                            if (Table.cells[x - 1, y - 1].checker is BlackChecker)
                                if (x > 1 && y > 1)
                                    if ((uncle != null ? uncle.myCell != Table.cells[x - 2, y - 2] : true)) // check if uncle havn't same cell
                                        if (Table.cells[x - 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x - 2, y - 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y - 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                        if (x < 7)
                        {
                            // move right
                            if (Table.cells[x + 1, y - 1].checker==null && uncle == null)
                                Game.allowedMoves.Add(new Move(this, Table.cells[x + 1, y - 1]));
                            // move forward right & kill
                            if (Table.cells[x + 1, y - 1].checker is BlackChecker)
                                if (x < 6 && y > 1)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y - 2] : true)
                                        if (Table.cells[x + 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x + 2, y - 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x + 2, y - 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                    }
                    if (y < 7)
                    {
                        //move backward left & kill
                        if (x > 0)
                            if (Table.cells[x - 1, y + 1].checker is BlackChecker)
                                if (x > 1 && y < 6)
                                    if (uncle != null ? uncle.myCell != Table.cells[x - 2, y + 2] : true)
                                        if (Table.cells[x - 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x - 2, y + 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y + 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        //move backward right & kill
                        if (x < 7)
                            if (Table.cells[x + 1, y + 1].checker is BlackChecker)
                                if (x < 6 && y < 6)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y + 2] : true)
                                        if (Table.cells[x + 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x + 2, y + 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x + 2, y + 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                    }
                }
                #endregion
            }
                #endregion
                #region Black
            else
            {
                #region Queen
                if (isQueen)
                {
                    #region backward left
                    if (x > 0 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x - 1, currY = y - 1; currX >= 0 && currY >= 0; currX--, currY--)
                            {
                                if (Table.cells[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y - 1; ix != currX; ix--, iy--)
                                    {
                                        if (Table.cells[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix + 1, iy + 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                    #endregion
                    #region bacward right
                    if (x < 7 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x + 1, currY = y - 1; currX <= 7 && currY >= 0; currX++, currY--)
                            {
                                if (Table.cells[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y - 1; ix != currX; ix++, iy--)
                                    {
                                        if (Table.cells[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix - 1, iy + 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                    #endregion
                    #region forward left
                    if (x > 0 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x - 1, currY = y + 1; currX >= 0 && currY <= 7; currX--, currY++)
                            {
                                if (Table.cells[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y + 1; ix != currX; ix--, iy++)
                                    {
                                        if (Table.cells[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix + 1, iy - 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                    #endregion
                    #region forward right
                    if (x < 7 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x + 1, currY = y + 1; currX <= 7 && currY <= 7; currX++, currY++)
                            {
                                if (Table.cells[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y + 1; ix != currX; ix++, iy++)
                                    {
                                        if (Table.cells[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(Table.getChecker(Table.cells[ix, iy]));
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(Table.getChecker(Table.cells[ix - 1, iy - 1]));
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        Game.allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new Checker(this, Table.cells[currX, currY]);
                                        nephewCh.computeAllowedMoves();
                                    }

                                }

                            }
                }
                    #endregion
                    #endregion
                    #region Simple
                else
                {
                    if (y < 7)
                    {
                        if (x > 0)
                        {
                            // move left
                            if (Table.cells[x - 1, y + 1].checker==null && uncle == null) // "virtual" checkers can onle beat, not go
                                Game.allowedMoves.Add(new Move(this, Table.cells[x - 1, y + 1]));
                            // move forward left & kill
                            if (Table.cells[x - 1, y + 1].checker is WhiteChecker)
                                if (x > 1 && y < 6)
                                    if ((uncle != null ? uncle.myCell != Table.cells[x - 2, y + 2] : true)) // check if uncle havn't same cell
                                        if (Table.cells[x - 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x - 2, y + 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y + 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                        if (x < 7)
                        {
                            // move right
                            if (Table.cells[x + 1, y + 1].checker==null && uncle == null)
                                Game.allowedMoves.Add(new Move(this, Table.cells[x + 1, y + 1]));
                            // move forward right & kill
                            if (Table.cells[x + 1, y + 1].checker is WhiteChecker)
                                if (x < 6 && y < 6)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y + 2] : true)
                                        if (Table.cells[x + 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x + 2, y + 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x + 2, y + 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                    }
                    if (y > 0)
                    {
                        //move backward left & kill
                        if (x > 0)
                            if (Table.cells[x - 1, y - 1].checker is WhiteChecker)
                                if (x > 1 && y > 1)
                                    if (uncle != null ? uncle.myCell != Table.cells[x - 2, y - 2] : true)
                                        if (Table.cells[x - 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x - 2, y - 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y - 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        //move backward right & kill
                        if (x < 7)
                            if (Table.cells[x + 1, y - 1].checker is WhiteChecker)
                                if (x < 6 && y > 1)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y - 2] : true)
                                        if (Table.cells[x + 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            Game.allowedMoves.Add(new Move(this, Table.cells[x + 2, y - 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x + 2, y - 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                    }
                }
                #endregion
            }
            #endregion
        }
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
                    Game.chsBlack.Remove(ch as BlackChecker);
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
                    Game.chsWhite.Remove(ch as WhiteChecker);
                }
                move.toCell.checker = this;
                if (move.toCell.GetNumber()[1] == 7)
                {
                    isQueen = true;
                    chBox.Image = global::checkers.Properties.Resources.black_checker_queen;
                }
            }

            // end of game
            if (Game.chsBlack.Count == 0)
                MessageBox.Show("White win");
            if (Game.chsWhite.Count == 0)
                MessageBox.Show("Black win");

            myCell = move.toCell;
            chBox.Location = new System.Drawing.Point(move.toCell.GetLocation()[0], move.toCell.GetLocation()[1]);

            Game.allowedMoves = new List<Move>();
            if (this is WhiteChecker)
                foreach (Checker ch in Game.chsBlack)
                    ch.computeAllowedMoves();
            else
                foreach (Checker ch in Game.chsWhite)
                    ch.computeAllowedMoves();
            #region rule: must kill, not go
            foreach (Move mv in Game.allowedMoves)
                if (mv.killedChs.Count > 0)
                {
                    for (int i = Game.allowedMoves.Count - 1; i >= 0; i--)
                        if (Game.allowedMoves[i].killedChs.Count == 0 || Game.allowedMoves[i].killedChs==null)
                            Game.allowedMoves.RemoveAt(i);
                    break;
                }

            #endregion
            Table.selected.unselect();
            Game.isWhite = !Game.isWhite;
        }
        /// <summary>
        /// method for selecting checker
        /// </summary>
        private void select()
        {
            chBox.BackColor = System.Drawing.SystemColors.ControlDark;
            isSelected = true;
            Table.selected = this;
        }
        /// <summary>
        /// method for unselecting checker
        /// </summary>
        public void unselect()
        {
            chBox.BackColor = System.Drawing.Color.Transparent;
            isSelected = false;
            Table.selected = null;
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
                if(Table.selected!=null)
                    Table.selected.unselect();
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
        /// <param name="ch">Real (uncle) checker, that's moving</param>
        /// <param name="cell">Cell, where nephew located</param>
        public Checker(Checker ch, Cell cell)
        {
            myCell = cell;
            uncle = ch;
            isQueen = ch.isQueen;
        }
        /// <summary>
        /// Get killed checkers by uncle
        /// </summary>
        /// <returns>killed checkers by uncle</returns>
        public List<Checker> getKilled()
        {
            if (uncle == null)
                return new List<Checker>();
            foreach (Move m in Game.allowedMoves)
                if (m.checker == uncle && myCell == m.toCell)
                    return m.killedChs;
            return null; // stop point for debug (uncles must have killed)
        }
        /// <summary>
        /// Delete same checkers(doublets) in list and return it
        /// </summary>
        /// <param name="killed">list which need to be cleaned</param>
        /// <returns>list without doublets</returns>
        public List<Checker> dleteDoublets(List<Checker> killed)
        {
            List<Checker> killedWithoutDoublets = new List<Checker>();
            foreach (Checker ch in killed)
                if (!killedWithoutDoublets.Contains(ch))
                    killedWithoutDoublets.Add(ch);
            return killedWithoutDoublets;
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