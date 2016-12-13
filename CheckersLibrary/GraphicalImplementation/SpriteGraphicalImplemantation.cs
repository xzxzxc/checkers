using System;

namespace CheckersLibrary.GraphicalImplementation
{
    public abstract class SpriteGraphicalImplemantation
    {
        /// <summary>
        /// Change background color
        /// </summary>
        /// <param name="color"></param>
        public abstract void ChancheBgColor(Color color);

        /// <summary>
        /// Get image of cell
        /// </summary>
        /// <returns></returns>
        public abstract Object GetImage();

        /// <summary>
        /// Click event handler
        /// </summary>
        public event Click SpriteClick;

        /// <summary>
        /// Method that invokes when cell was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Click(Object sender, EventArgs e)
        {
            SpriteClick.Invoke();
        }
    }
}
