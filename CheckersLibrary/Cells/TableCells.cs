using CheckersLibrary.GraphicalImplementation;

namespace CheckersLibrary.Cells
{
    /// <summary>
    /// Class for collecting and getting cells
    /// </summary>
    public class CellManager
    {
        /// <summary>
        /// List of cells on table
        /// </summary>
        private static readonly Cell[,] Cells = new Cell[8, 8]; 
        /// <summary>
        /// Constructor, create cells
        /// </summary>
        public CellManager(CellGraphicalImplementation[,] cellGraphicalImplementation)
        {
            for (byte i = 0; i < 8; ++i)
                for (byte j = 0; j < 8; ++j)
                    Cells[i, j] = new Cell(i, j, cellGraphicalImplementation[i, j]);
        }
        /// <summary>
        /// Get cell by coordinates
        /// </summary>
        /// <param name="a">Cell x coordinate on table</param>
        /// <param name="b">Cell y coordinate on table</param>
        /// <returns>Coresponding cell</returns>
        public Cell this[int a, int b] => Cells[a, b];
    }
    /// <summary>
    /// Class for controling game table
    /// </summary>
    public static class TableCells
    {
        /// <summary>
        /// Get cell by coordinates
        /// </summary>
        public static CellManager Cell { get; private set; } = null;

        public static void Create(CellGraphicalImplementation[,] cellGraphicalImplementation)
        {
            if (Cell == null)
                Cell = new CellManager(cellGraphicalImplementation);
        }

        /// <summary>
        /// Clear all cells
        /// </summary>
        public static void Clear()
        {
            for (byte i = 0; i < 8; ++i)
                for (byte j = 0; j < 8; ++j)
                    if (!Cell[i, j].IsClear())
                        Cell[i, j].Clear();
        }
        /// <summary>
        /// Draw table in window
        /// </summary>
        public static void Draw()
        {
            for (byte i = 0; i < 8; ++i)
                for (byte j = 0; j < 8; ++j)
                    Cell[i, j].Draw(i, j);
        }
    }
}