using System;
using CheckersLibrary.Checkers;

namespace CheckersLibrary.GraphicalImplementation
{
    /// <summary>
    /// Class that contains abstract cell graphical implementation
    /// </summary>
    public abstract class CellGraphicalImplementation : SpriteGraphicalImplemantation
    {
        /// <summary>
        /// Remove checker from cell
        /// </summary>
        public abstract void RemoveChecker();

        /// <summary>
        /// Add checker image to cell image
        /// </summary>
        /// <param name="checker"></param>
        public abstract void AddChecker(Checker checker);

        /// <summary>
        /// Draw cell in window
        /// </summary>
        public abstract void Draw(int x, int y);
    }
}