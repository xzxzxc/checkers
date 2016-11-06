using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{

    /// <summary>
    /// Class for controling possible moves
    /// </summary>
    class Move
    {
        public Checker checker; // what chacker is moving
        public Cell toCell; // where checker move
        public List<Checker> killedChs; // what checker will be killed

        public Move(Checker checker, Cell toCell, List<Checker> killedChs = null)
        {
            this.checker = checker;
            this.toCell = toCell;
            if (killedChs != null)
                this.killedChs = killedChs;
            else
                this.killedChs = new List<Checker>();
        }

        /// <summary>
        /// Get non-virtual (super uncle) checker of move
        /// </summary>
        /// <returns>Non-virtual (super uncle) checker</returns>
        public Checker getSuperUncle()
        {
            Checker currCh = checker;
            while (currCh.uncle != null)
                currCh = currCh.uncle;
            return currCh;
        }


    }
    /// <summary>
    /// Class for controling cells in table
    /// </summary>
    class Cell
    {
        private int[] location; // location in windows form
        private int[] number; // number on table
        public bool whiteCh = false; // is white checker on this cell
        public bool blackCh = false; // is black checker on this cell

        public Cell(int x, int y)
        {
            number = new int[] { x, y };
            location = new int[] { x * 75, y * 75};
        }

        public int[] GetLocation()
        {
            return location;
        }

        public int[] GetNumber()
        {
            return number;
        }
        /// <summary>
        /// checking if this cell is clear
        /// </summary>
        /// <returns>true if clear, false if not</returns>
        public bool clear()
        {
            if (!whiteCh && !blackCh)
                return true;
            return false;
        }
    }
    /// <summary>
    /// class for controling checkers
    /// </summary>
    class Checker
    {
        private bool isWhite; // true if white and false if black
        public bool isQueen = false; // true if queen and false if simple
        public bool isSelected = false; // true if selected and false if not
        public PictureBox chBox; // image of checker for windows form
        static public List<Move> allowedMoves = new List<Move>(); // list of alowed moves for all checkers
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
            if (isWhite)
            {
                #region Queen
                if (isQueen)
                {
                    #region forward left
                    if (x > 0 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x - 1, currY = y - 1; currX >= 0 && currY >= 0; currX--, currY--)
                            {
                                if (Table.cells[currX, currY].clear())
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y - 1; ix != currX; ix--, iy--)
                                    {
                                        if (Table.cells[ix, iy].blackCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                                if (Table.cells[currX, currY].clear()) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y - 1; ix != currX; ix++, iy--)
                                    {
                                        if (Table.cells[ix, iy].blackCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                                if (Table.cells[currX, currY].clear())
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y + 1; ix != currX; ix--, iy++)
                                    {
                                        if (Table.cells[ix, iy].blackCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                                if (Table.cells[currX, currY].clear()) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y + 1; ix != currX; ix++, iy++)
                                    {
                                        if (Table.cells[ix, iy].blackCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                            if (Table.cells[x - 1, y - 1].clear() && uncle == null) // "virtual" checkers can onle beat, not go
                                allowedMoves.Add(new Move(this, Table.cells[x - 1, y - 1]));
                            // move forward left & kill
                            if (Table.cells[x - 1, y - 1].blackCh)
                                if (x > 1 && y > 1)
                                    if ((uncle != null ? uncle.myCell != Table.cells[x - 2, y - 2] : true)) // check if uncle havn't same cell
                                        if (Table.cells[x - 2, y - 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x - 2, y - 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y - 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                        if (x < 7)
                        {
                            // move right
                            if (Table.cells[x + 1, y - 1].clear() && uncle == null)
                                allowedMoves.Add(new Move(this, Table.cells[x + 1, y - 1]));
                            // move forward right & kill
                            if (Table.cells[x + 1, y - 1].blackCh)
                                if (x < 6 && y > 1)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y - 2] : true)
                                        if (Table.cells[x + 2, y - 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x + 2, y - 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x + 2, y - 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                    }
                    if (y < 7)
                    {
                        //move backward left & kill
                        if (x > 0)
                            if (Table.cells[x - 1, y + 1].blackCh)
                                if (x > 1 && y < 6)
                                    if (uncle != null ? uncle.myCell != Table.cells[x - 2, y + 2] : true)
                                        if (Table.cells[x - 2, y + 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x - 2, y + 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y + 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        //move backward right & kill
                        if (x < 7)
                            if (Table.cells[x + 1, y + 1].blackCh)
                                if (x < 6 && y < 6)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y + 2] : true)
                                        if (Table.cells[x + 2, y + 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x + 2, y + 2], killed));
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
                                if (Table.cells[currX, currY].clear())
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y - 1; ix != currX; ix--, iy--)
                                    {
                                        if (Table.cells[ix, iy].whiteCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                                if (Table.cells[currX, currY].clear()) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y - 1; ix != currX; ix++, iy--)
                                    {
                                        if (Table.cells[ix, iy].whiteCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                                if (Table.cells[currX, currY].clear())
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y + 1; ix != currX; ix--, iy++)
                                    {
                                        if (Table.cells[ix, iy].whiteCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                                if (Table.cells[currX, currY].clear()) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y + 1; ix != currX; ix++, iy++)
                                    {
                                        if (Table.cells[ix, iy].whiteCh)
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
                                        allowedMoves.Add(new Move(this, Table.cells[currX, currY], killed));
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
                            if (Table.cells[x - 1, y + 1].clear() && uncle == null) // "virtual" checkers can onle beat, not go
                                allowedMoves.Add(new Move(this, Table.cells[x - 1, y + 1]));
                            // move forward left & kill
                            if (Table.cells[x - 1, y + 1].whiteCh)
                                if (x > 1 && y < 6)
                                    if ((uncle != null ? uncle.myCell != Table.cells[x - 2, y + 2] : true)) // check if uncle havn't same cell
                                        if (Table.cells[x - 2, y + 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x - 2, y + 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y + 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                        if (x < 7)
                        {
                            // move right
                            if (Table.cells[x + 1, y + 1].clear() && uncle == null)
                                allowedMoves.Add(new Move(this, Table.cells[x + 1, y + 1]));
                            // move forward right & kill
                            if (Table.cells[x + 1, y + 1].whiteCh)
                                if (x < 6 && y < 6)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y + 2] : true)
                                        if (Table.cells[x + 2, y + 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y + 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x + 2, y + 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x + 2, y + 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        }
                    }
                    if (y > 0)
                    {
                        //move backward left & kill
                        if (x > 0)
                            if (Table.cells[x - 1, y - 1].whiteCh)
                                if (x > 1 && y > 1)
                                    if (uncle != null ? uncle.myCell != Table.cells[x - 2, y - 2] : true)
                                        if (Table.cells[x - 2, y - 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x - 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x - 2, y - 2], killed));
                                            Checker nephewCh = new Checker(this, Table.cells[x - 2, y - 2]);
                                            nephewCh.computeAllowedMoves();
                                        }
                        //move backward right & kill
                        if (x < 7)
                            if (Table.cells[x + 1, y - 1].whiteCh)
                                if (x < 6 && y > 1)
                                    if (uncle != null ? uncle.myCell != Table.cells[x + 2, y - 2] : true)
                                        if (Table.cells[x + 2, y - 2].clear())
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(Table.getChecker(Table.cells[x + 1, y - 1]));
                                            killed.AddRange(getKilled()); // get uncle killed
                                            allowedMoves.Add(new Move(this, Table.cells[x + 2, y - 2], killed));
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
        public void move(Move move)
        {
            if (isWhite)
            {
                myCell.whiteCh = false;
                foreach (Checker ch in move.killedChs)
                {
                    ch.myCell.blackCh = false;
                    ch.chBox.Visible = false;
                    Table.chsBlack.Remove(ch);
                }
                move.toCell.whiteCh = true;
                if (move.toCell.GetNumber()[1] == 0)
                {
                    isQueen = true;
                    chBox.Image = global::checkers.Properties.Resources.white_checker_queen;
                }
            }
            else
            {
                myCell.blackCh = false;
                foreach (Checker ch in move.killedChs)
                {
                    ch.myCell.whiteCh = false;
                    ch.chBox.Visible = false;
                    Table.chsWhite.Remove(ch);
                }
                move.toCell.blackCh = true;
                if (move.toCell.GetNumber()[1] == 7)
                {
                    isQueen = true;
                    chBox.Image = global::checkers.Properties.Resources.black_checker_queen;
                }
            }

            // end of game
            if (Table.chsBlack.Count == 0)
                MessageBox.Show("White win");
            if (Table.chsWhite.Count == 0)
                MessageBox.Show("Black win");

            myCell = move.toCell;
            chBox.Location = new System.Drawing.Point(move.toCell.GetLocation()[0], move.toCell.GetLocation()[1]);

            Checker.allowedMoves = new List<Move>();
            if (isWhite)
                foreach (Checker ch in Table.chsBlack)
                    ch.computeAllowedMoves();
            else
                foreach (Checker ch in Table.chsWhite)
                    ch.computeAllowedMoves();
            #region rule: must kill, not go
            foreach (Move mv in Checker.allowedMoves)
                if (mv.killedChs.Count > 0)
                {
                    for (int i = Checker.allowedMoves.Count - 1; i >= 0; i--)
                        if (Checker.allowedMoves[i].killedChs.Count == 0 || allowedMoves[i].killedChs==null)
                            Checker.allowedMoves.RemoveAt(i);
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
            if (Game.isWhite == isWhite)
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
        public Checker(Cell myCell, char color)
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

            
            if (color == 'w')
            {
                isWhite = true;
                chBox.Image = global::checkers.Properties.Resources.white_checker;
            }
            else if (color == 'b')
            {
                isWhite = false;
                chBox.Image = global::checkers.Properties.Resources.black_checker;
            }
            else
                throw new ArgumentException("Checker must be black('b') or white('w')");
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
            isWhite = ch.isWhite;
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
            foreach (Move m in allowedMoves)
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
    /// <summary>
    /// Class for controling game table
    /// </summary>
    static class Table
    {
        static public PictureBox tableBox; // table pic
        static public List <Checker> chsWhite = new List<Checker>(); // list of white checkers on table
        static public List <Checker> chsBlack = new List<Checker>(); // list of black checkers on table
        static public Cell[,] cells = new Cell[8, 8]; // list of cells on tanle
        static public Checker selected; // current selected checker
        /// <summary>
        /// Method for deleting checker from table
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        static public void delChecker(int x, int y)
        {
            Cell cell = cells[x, y];
            for (int i = chsBlack.Count - 1; i >= 0; --i)
                if (chsBlack[i].myCell == cell)
                {
                    chsBlack[i].chBox.Visible = false;
                    chsBlack.RemoveAt(i);
                }
            for (int i = chsWhite.Count - 1; i >=0; --i)
                if (chsWhite[i].myCell == cell)
                {
                    chsWhite[i].chBox.Visible = false;
                    chsWhite.RemoveAt(i);
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
            Checker selectedCh = null;
            if (Game.isWhite)
            {
                foreach (Checker ch in chsWhite)
                    if (ch.isSelected)
                        selectedCh = ch;
            }
            else
            {
                foreach (Checker ch in chsBlack)
                    if (ch.isSelected)
                        selectedCh = ch;
            }
            if (selectedCh == null)
                return;
            foreach (Move m in Checker.allowedMoves)
                if (m.toCell == clickCell && m.getSuperUncle() == selectedCh)
                {
                    selectedCh.move(m);
                    break;
                }
        }
        /// <summary>
        /// Create table
        /// </summary>
        static public void CreateTable()
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
        static public Checker getChecker(Cell cell)
        {
            foreach (Checker ch in chsBlack)
                if (ch.myCell == cell)
                    return ch;
            foreach (Checker ch in chsWhite)
                if (ch.myCell == cell)
                    return ch;
            MessageBox.Show("no checker");
            return null;
        }
    }
    /// <summary>
    /// Class for controling the game (for implementation save/load functionality in future)
    /// </summary>
    static class Game
    {
        static public bool isWhite = true; // for controling whose turn
        /// <summary>
        /// Start new game
        /// </summary>
        static public void StartGame()
        {
            Table.CreateTable();
            // dispose checkers
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    Cell wCell = Table.cells[2 * j + i % 2, 7 - i];
                    wCell.whiteCh = true;
                    Table.chsWhite.Add(new Checker(wCell, 'w'));
                    Cell bCell = Table.cells[1 + 2 * j - i % 2, i];
                    bCell.blackCh = true;
                    Table.chsBlack.Add(new Checker(bCell, 'b'));
                }

            foreach (Checker ch in Table.chsWhite)
                ch.computeAllowedMoves();
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
