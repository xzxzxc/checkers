using System.Collections.Generic;
using System.Windows.Forms;

namespace checkers
{
    /// <summary>
    /// Class for controling possible moves
    /// </summary>
    public class Move
    {
        public Checker Checker { get; } // what chacker is moving
        public Cell toCell; // where checker move
        public List<Checker> killedChs; // what checkers will be killed

        public Move(Checker checker, Cell toCell, List<Checker> killedChs)
        {
            this.Checker = checker;
            this.toCell = toCell;
            this.killedChs = killedChs;
        }

        public Move(Checker checker, Cell toCell) : this(checker, toCell, new List<Checker>()) { }
    }
}