using System.Windows.Forms;

namespace checkers
{
    /// <summary>
    /// Class for controling cells in table
    /// </summary>
    public class Cell
    {
        private int[] location; // location in windows form
        private int[] number; // number on table
        public Checker checker = null;

        public Cell(int x, int y)
        {
            number = new int[] { x, y };
            location = new int[] { x * 75, y * 75 };
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

    public class CellManager
    {
        private static Cell[,] cells = new Cell[8, 8]; // list of cells on tanle

        public CellManager()
        {
            for (int i = 0; i < 8; ++i)
                for (int j = 0; j < 8; ++j)
                    cells[i, j] = new Cell(i, j);
        }

        public Cell this[int a, int b]
        {
            get { return cells[a, b]; }
        }
    }

    public static class TableCells
    {
        static CellManager _cellManager = new CellManager();
        public static CellManager Cell { get { return _cellManager; } }

        public static void Clear()
        {
            _cellManager = new CellManager();
        }
    }
}