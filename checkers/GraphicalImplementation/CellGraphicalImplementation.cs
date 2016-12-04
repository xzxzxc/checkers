using System;
using System.Drawing;
using checkers.Checkers;

namespace checkers.GraphicalImplementation
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
        /// Current cell checker
        /// </summary>
        protected Checker _checker;
        /// <summary>
        /// Get checker
        /// </summary>
        public Checker Checker => _checker;

        /// <summary>
        /// Draw cell in window
        /// </summary>
        public abstract void Draw();
    }
}