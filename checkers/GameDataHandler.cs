﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers
{
    public struct GameDataHandler
    {
        public static LinkedList<Move> previousMoves; // list of previous moves
        public static List<WhiteChecker> chsWhite; // list of white checkers on table
        public static List<BlackChecker> chsBlack; // list of black checkers on table
        public static List<Move> allowedMoves; // list of alowed moves for all checkers
        public static Checker selected = null; // current selected checker
    }
}