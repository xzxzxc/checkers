using CheckersLibrary;
using CheckersLibrary.Checkers;
using CheckersLibrary.GraphicalImplementation;

namespace CheckersTest
{
    public class TestCellImplementation : CellGraphicalImplementation
    {
        public override void ChancheBgColor(Color color) { }

        public override object GetImage()
        {
            return null;
        }

        public override void RemoveChecker() { }

        public override void AddChecker(Checker checker) { }

        public override void Draw(int x, int y) { }
    }
}