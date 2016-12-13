using CheckersLibrary.Cells;

namespace CheckersLibrary.GraphicalImplementation
{
    public abstract class CheckerGraphicalImplementation : SpriteGraphicalImplemantation
    {
        /// <summary>
        /// Draw checker in window
        /// </summary>
        /// <param name="checkerColor"></param>
        public abstract void Draw(Color checkerColor, Cell cell);
        /// <summary>
        /// Change image to queen image
        /// </summary>
        public abstract void BeQueen();
        /// <summary>
        /// Change image to simple checker image
        /// </summary>
        public abstract void UnBeQueen();
        /// <summary>
        /// Set visability
        /// </summary>
        public abstract bool Visible { set; }
        
    }
}