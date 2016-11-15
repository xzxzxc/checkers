using System;

namespace checkers
{
    /// <summary>
    /// Class for controling cells in table
    /// </summary>
    public class Cell
    {
        private int[] location; // location in windows form
        private int[] number; // number on table
        public Checker checker=null;

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
    }
}