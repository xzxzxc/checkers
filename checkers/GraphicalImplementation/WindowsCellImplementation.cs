using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using checkers.Checkers;

namespace checkers.GraphicalImplementation
{
    public class WindowsCellImplementation : CellGraphicalImplementation
    {
        private readonly PictureBox _cellBox; // image of cell for windows form
        private readonly int _x;  // x location in windows form
        private readonly int _y;  // y location in windows form

        private void InitializeCellBox()
        {
            ((ISupportInitialize)(_cellBox)).BeginInit();
            _cellBox.BackColor = _y % 2 + _x % 2 == 1 ? Color.Gray : Color.White;
            _cellBox.Location = new Point(_x*75, _y*75);
            _cellBox.Name = "cellBox";
            _cellBox.Size = new Size(75, 75);
            _cellBox.SizeMode = PictureBoxSizeMode.Zoom;
            _cellBox.TabIndex = 1;
            _cellBox.TabStop = false;
            _cellBox.Click += Click;
            ((ISupportInitialize)(_cellBox)).EndInit();
        }

        public WindowsCellImplementation(int x, int y)
        {
            _cellBox = new PictureBox();
            _x = x;
            _y = y;
        }
        public override void Draw()
        {
            InitializeCellBox();
        }

        public override void RemoveChecker()
        {
            if (_checker != null)
            {
                _cellBox.Controls.Remove((PictureBox) _checker.GetImage());
                _checker = null;
            }
        }

        public override void ChancheBgColor(Color color)
        {
            _cellBox.BackColor = color;
        }

        public override void AddChecker(Checker checker)
        {
            if (_checker != null)
                throw new AccessViolationException("Cell already contains checker");
            _checker = checker;
            _cellBox.Controls.Add((PictureBox)checker.GetImage());
        }
        public override Object GetImage()
        {
            return _cellBox;
        }
    }
}