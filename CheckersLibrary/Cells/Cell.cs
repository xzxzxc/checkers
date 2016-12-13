using System;
using CheckersLibrary.Checkers;
using CheckersLibrary.GraphicalImplementation;

namespace CheckersLibrary.Cells
{
    /// <summary>
    /// Class for controling cells in table
    /// </summary>
    public class Cell
    {
        private Checker _checker;
        /// <summary>
        /// Checker on cell if exists
        /// </summary>
        public Checker Checker
        {
            get { return _checker; }
            set
            {
                if (value == null)
                {
                    _graphicalImpl.RemoveChecker();
                    _checker = null;
                }
                else
                {
                    _graphicalImpl.AddChecker(value);
                    _checker = value;
                }
            } 
        }
        /// <summary>
        /// Cell x coordinate on table
        /// </summary>
        public readonly byte X;
        /// <summary>
        /// Cell y coordinate on table
        /// </summary>
        public readonly byte Y;
        /// <summary>
        /// Cell graphical implementation
        /// </summary>
        private readonly CellGraphicalImplementation _graphicalImpl;
        /// <summary>
        /// Default click event handler
        /// </summary>
        public event Click Click;
        /// <summary>
        /// Calls, when cell was pressed
        /// </summary>
        private void SpriteClick()
        {
           Click?.Invoke();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">Cell x coordinate on table</param>
        /// <param name="y">Cell y coordinate on table</param>
        public Cell(byte x, byte y, CellGraphicalImplementation cellGraphicalImplementation)
        {
            _graphicalImpl = cellGraphicalImplementation;
            X = x; Y = y;
            _graphicalImpl.SpriteClick += SpriteClick;
        }
        /// <summary>
        /// Get cell image
        /// </summary>
        /// <returns>Cell image</returns>
        public Object GetImage()
        {
            return _graphicalImpl.GetImage();
        }
        /// <summary>
        /// Check if cell is clear
        /// </summary>
        /// <returns>Yrue if yes, false if not</returns>
        public bool IsClear()
        {
            return _checker == null;
        }
        /// <summary>
        /// Remove checker from cell
        /// </summary>
        public void Clear()
        {
            if (IsClear()) return;
            _graphicalImpl.RemoveChecker();
            _checker = null;
        }
        /// <summary>
        /// Change background collor of cell
        /// </summary>
        /// <param name="color"></param>
        public void ChangeBgColor(Color color)
        {
            _graphicalImpl.ChancheBgColor(color);
        }
        /// <summary>
        /// Draw cell in window
        /// </summary>
        public void Draw(int x, int y)
        {
            _graphicalImpl.Draw(x, y);
        }
    }

}