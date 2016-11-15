using System.Collections.Generic;

namespace checkers
{
    /// <summary>
    /// Class for controling possible moves
    /// </summary>
    public class Move
    {
        public Checker checker; // what chacker is moving
        public Cell toCell; // where checker move
        public List<Checker> killedChs; // what checkers will be killed

        public Move(Checker checker, Cell toCell, List<Checker> killedChs)
        {
            this.checker = checker;
            this.toCell = toCell;
            this.killedChs = killedChs;
        }

        public Move(Checker checker, Cell toCell) : this(checker, toCell, new List<Checker>()) { }

        /// <summary>
        /// Get non-virtual (super uncle) checker of move
        /// </summary>
        /// <returns>Non-virtual (super uncle) checker</returns>
        public Checker getSuperUncle()
        {
            Checker currCh = checker;
            while (currCh.uncle != null)
                currCh = currCh.uncle;
            return currCh;
        }


    }
}