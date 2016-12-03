using System;
using System.Windows.Forms;

namespace checkers.Cells
{
    public class CellManager
    {
        private static readonly Cell[,] Cells = new Cell[8, 8]; // list of cells on tanle

        public CellManager()
        {
            for (byte i = 0; i < 8; ++i)
                for (byte j = 0; j < 8; ++j)
                    Cells[i, j] = new Cell(i, j);
        }

        public Cell this[int a, int b] => Cells[a, b];
    }

    public static class TableCells
    {
        public static CellManager Cell { get; private set; } = new CellManager();

        public static void Clear()
        {
            Cell = new CellManager();
        }

        public static void Draw()
        {
            for (byte i = 0; i < 8; ++i)
                for (byte j = 0; j < 8; ++j)
                    Cell[i, j].Draw();
        }
    }
}