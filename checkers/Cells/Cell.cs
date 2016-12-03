using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using checkers.Checkers;
using checkers.GraphicalImplementation;

namespace checkers.Cells
{
    /// <summary>
    /// Class for controling cells in table
    /// </summary>
    public class Cell
    {
        public Checker Checker
        {
            get { return _graphicalImpl.Checker; }
            set
            {
                if (value == null)
                    _graphicalImpl.RemoveChecker();
                else
                    _graphicalImpl.AddChecker(value);
            } 
        }
        public readonly byte X;
        public readonly byte Y;
        private CellGraphicalImplementation _graphicalImpl;
        public event Click Click;

        public void CellClick()
        {
           Click?.Invoke();
        }

        public Cell(byte x, byte y)
        {
            _graphicalImpl = new WindowsCellImplementation(x, y);
            X = x; Y = y;
            _graphicalImpl.CellClick += CellClick;
        }

        public Object GetImage()
        {
            return _graphicalImpl.GetImage();
        }

        public bool IsClear()
        {
            return Checker == null;
        }
        
        public void Clear()
        {
            _graphicalImpl.RemoveChecker();
        }

        public void ChangeBgColor(Color color)
        {
            _graphicalImpl.ChancheBgColor(color);
        }

        public void Draw()
        {
            _graphicalImpl.CellDraw();
        }
    }

}