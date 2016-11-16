using System;
using System.Collections.Generic;

namespace checkers
{
    public static class AllowedMovesCalculator
    {
        private static Checker _checker;
        private static Checker _superUncle; // uncle checker (using when computing allowed moves)

        /*public AllowedMovesCalculator(Checker checker)
        {
            checker = checker;
        }

        public AllowedMovesCalculator(Checker uncle, Checker checker)
        {
            this.uncle = uncle;
            checker = checker;
        }*/

        /// <summary>
        /// computing allowed moves and appending them to corresponding list
        /// </summary>
        public static void Calculate(Checker checker, Checker uncle = null)
        {
            if (_checker != checker && uncle==null)
                _superUncle = null;
            _checker = checker;
            if (_superUncle == null && uncle != null) //
                _superUncle = uncle;
            int x = checker.myCell.GetNumber()[0];
            int y = checker.myCell.GetNumber()[1];
            #region White
            if (checker is WhiteChecker)
            {
                #region Queen
                if (checker.isQueen)
                {
                    #region forward left
                    if (x > 0 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x - 1, currY = y - 1; currX >= 0 && currY >= 0; currX--, currY--)
                            {
                                if (TableCells.Cell[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y - 1; ix != currX; ix--, iy--)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix + 1, iy + 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
                                    }

                                }

                            }
                    #endregion
                    #region forward right
                    if (x < 7 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x + 1, currY = y - 1; currX <= 7 && currY >= 0; currX++, currY--)
                            {
                                if (TableCells.Cell[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y - 1; ix != currX; ix++, iy--)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix - 1, iy + 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
                                    }

                                }

                            }
                    #endregion
                    #region backward left
                    if (x > 0 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x - 1, currY = y + 1; currX >= 0 && currY <= 7; currX--, currY++)
                            {
                                if (TableCells.Cell[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y + 1; ix != currX; ix--, iy++)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix + 1, iy - 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
                                    }

                                }

                            }
                    #endregion
                    #region backward right
                    if (x < 7 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x + 1, currY = y + 1; currX <= 7 && currY <= 7; currX++, currY++)
                            {
                                if (TableCells.Cell[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y + 1; ix != currX; ix++, iy++)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is BlackChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix - 1, iy - 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    //killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
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
                            if (TableCells.Cell[x - 1, y - 1].checker==null && _superUncle == null) // "virtual" checkers can onle beat, not go
                                GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x - 1, y - 1]));
                            // move forward left & kill
                            if (TableCells.Cell[x - 1, y - 1].checker is BlackChecker)
                                if (x > 1 && y > 1)
                                    if ((uncle != null ? uncle.myCell != TableCells.Cell[x - 2, y - 2] : true)) // check if uncle havn't same cell
                                        if (TableCells.Cell[x - 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x - 1, y - 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x - 2, y - 2], killed));
                                            Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[x - 2, y - 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
                                        }
                        }
                        if (x < 7)
                        {
                            // move right
                            if (TableCells.Cell[x + 1, y - 1].checker==null && _superUncle == null)
                                GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x + 1, y - 1]));
                            // move forward right & kill
                            if (TableCells.Cell[x + 1, y - 1].checker is BlackChecker)
                                if (x < 6 && y > 1)
                                    if (uncle != null ? uncle.myCell != TableCells.Cell[x + 2, y - 2] : true)
                                        if (TableCells.Cell[x + 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x + 1, y - 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x + 2, y - 2], killed));
                                            Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[x + 2, y - 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
                                        }
                        }
                    }
                    if (y < 7)
                    {
                        //move backward left & kill
                        if (x > 0)
                            if (TableCells.Cell[x - 1, y + 1].checker is BlackChecker)
                                if (x > 1 && y < 6)
                                    if (uncle != null ? uncle.myCell != TableCells.Cell[x - 2, y + 2] : true)
                                        if (TableCells.Cell[x - 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x - 1, y + 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x - 2, y + 2], killed));
                                            Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[x - 2, y + 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
                                        }
                        //move backward right & kill
                        if (x < 7)
                            if (TableCells.Cell[x + 1, y + 1].checker is BlackChecker)
                                if (x < 6 && y < 6)
                                    if (uncle != null ? uncle.myCell != TableCells.Cell[x + 2, y + 2] : true)
                                        if (TableCells.Cell[x + 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x + 1, y + 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x + 2, y + 2], killed));
                                            Checker nephewCh = new WhiteChecker(checker, TableCells.Cell[x + 2, y + 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
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
                if (checker.isQueen)
                {
                    #region backward left
                    if (x > 0 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x - 1, currY = y - 1; currX >= 0 && currY >= 0; currX--, currY--)
                            {
                                if (TableCells.Cell[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y - 1; ix != currX; ix--, iy--)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix + 1, iy + 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new BlackChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
                                    }

                                }

                            }
                    #endregion
                    #region bacward right
                    if (x < 7 && y > 0)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x + 1, currY = y - 1; currX <= 7 && currY >= 0; currX++, currY--)
                            {
                                if (TableCells.Cell[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y - 1; ix != currX; ix++, iy--)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix - 1, iy + 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new BlackChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
                                    }

                                }

                            }
                    #endregion
                    #region forward left
                    if (x > 0 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) > 0 : true))
                            for (int currX = x - 1, currY = y + 1; currX >= 0 && currY <= 7; currX--, currY++)
                            {
                                if (TableCells.Cell[currX, currY].checker==null)
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false; // two blacks on move
                                    for (int ix = x - 1, iy = y + 1; ix != currX; ix--, iy++)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix + 1, iy - 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new BlackChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
                                    }

                                }

                            }
                    #endregion
                    #region forward right
                    if (x < 7 && y < 7)
                        if ((uncle != null ? (uncle.myCell.GetNumber()[0] - x) * (uncle.myCell.GetNumber()[1] - y) < 0 : true))
                            for (int currX = x + 1, currY = y + 1; currX <= 7 && currY <= 7; currX++, currY++)
                            {
                                if (TableCells.Cell[currX, currY].checker==null) // "virtual" checkers can onle beat, not go
                                {
                                    List<Checker> killed = new List<Checker>();
                                    bool obom = false; // one black on move
                                    bool tbom = false;
                                    for (int ix = x + 1, iy = y + 1; ix != currX; ix++, iy++)
                                    {
                                        if (TableCells.Cell[ix, iy].checker is WhiteChecker)
                                            if (obom)
                                            {
                                                tbom = true;
                                                break;
                                            }
                                            else
                                            {
                                                obom = true;
                                                killed.Add(TableCells.Cell[ix, iy].checker);
                                                killed.AddRange(getKilled());
                                            }
                                        else
                                        {
                                            if (obom)
                                            {
                                                killed.Add(TableCells.Cell[ix - 1, iy - 1].checker);
                                                killed.AddRange(getKilled()); // get uncle killed
                                            }

                                            obom = false;
                                        }
                                    }
                                    if (tbom)
                                        break;
                                    killed = dleteDoublets(killed);
                                    if (uncle != null ? killed.Count > getKilled().Count : true)
                                        GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[currX, currY], killed));
                                    if (killed.Count > getKilled().Count)
                                    {
                                        Checker nephewCh = new BlackChecker(checker, TableCells.Cell[currX, currY]);
                                        AllowedMovesCalculator.Calculate(nephewCh, checker);
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
                            if (TableCells.Cell[x - 1, y + 1].checker==null && _superUncle == null) // "virtual" checkers can onle beat, not go
                                GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x - 1, y + 1]));
                            // move forward left & kill
                            if (TableCells.Cell[x - 1, y + 1].checker is WhiteChecker)
                                if (x > 1 && y < 6)
                                    if ((uncle != null ? uncle.myCell != TableCells.Cell[x - 2, y + 2] : true)) // check if uncle havn't same cell
                                        if (TableCells.Cell[x - 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x - 1, y + 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x - 2, y + 2], killed));
                                            Checker nephewCh = new BlackChecker(checker, TableCells.Cell[x - 2, y + 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
                                        }
                        }
                        if (x < 7)
                        {
                            // move right
                            if (TableCells.Cell[x + 1, y + 1].checker==null && _superUncle == null)
                                GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x + 1, y + 1]));
                            // move forward right & kill
                            if (TableCells.Cell[x + 1, y + 1].checker is WhiteChecker)
                                if (x < 6 && y < 6)
                                    if (uncle != null ? uncle.myCell != TableCells.Cell[x + 2, y + 2] : true)
                                        if (TableCells.Cell[x + 2, y + 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x + 1, y + 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x + 2, y + 2], killed));
                                            Checker nephewCh = new BlackChecker(checker, TableCells.Cell[x + 2, y + 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
                                        }
                        }
                    }
                    if (y > 0)
                    {
                        //move backward left & kill
                        if (x > 0)
                            if (TableCells.Cell[x - 1, y - 1].checker is WhiteChecker)
                                if (x > 1 && y > 1)
                                    if (uncle != null ? uncle.myCell != TableCells.Cell[x - 2, y - 2] : true)
                                        if (TableCells.Cell[x - 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x - 1, y - 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x - 2, y - 2], killed));
                                            Checker nephewCh = new BlackChecker(checker, TableCells.Cell[x - 2, y - 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
                                        }
                        //move backward right & kill
                        if (x < 7)
                            if (TableCells.Cell[x + 1, y - 1].checker is WhiteChecker)
                                if (x < 6 && y > 1)
                                    if (uncle != null ? uncle.myCell != TableCells.Cell[x + 2, y - 2] : true)
                                        if (TableCells.Cell[x + 2, y - 2].checker==null)
                                        {
                                            List<Checker> killed = new List<Checker>();
                                            killed.Add(TableCells.Cell[x + 1, y - 1].checker);
                                            killed.AddRange(getKilled()); // get uncle killed
                                            GameDataHandler.allowedMoves.Add(new Move((_superUncle==null)?checker:_superUncle, TableCells.Cell[x + 2, y - 2], killed));
                                            Checker nephewCh = new BlackChecker(checker, TableCells.Cell[x + 2, y - 2]);
                                            AllowedMovesCalculator.Calculate(nephewCh, checker);
                                        }
                    }
                }
                #endregion
            }
            #endregion
        }



        /// <summary>
        /// Get killed checkers by uncle
        /// </summary>
        /// <returns>killed checkers by uncle</returns>
        public static List<Checker> getKilled()
        {
            if (_superUncle == null)
                return new List<Checker>();
            foreach (Move m in GameDataHandler.allowedMoves)
                if (m.Checker == _superUncle && m.killedChs.Count > 0)
                    return m.killedChs;
            throw new Exception("Uncle must have killed");
        }

        /// <summary>
        /// Delete same checkers(doublets) in list and return it
        /// </summary>
        /// <param name="killed">list which need to be cleaned</param>
        /// <returns>list without doublets</returns>
        public static List<Checker> dleteDoublets(List<Checker> killed)
        {
            List<Checker> killedWithoutDoublets = new List<Checker>();
            foreach (Checker ch in killed)
                if (!killedWithoutDoublets.Contains(ch))
                    killedWithoutDoublets.Add(ch);
            return killedWithoutDoublets;
        }
    }
}