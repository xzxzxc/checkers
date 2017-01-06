using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CheckersLibrary.Checkers;
using CheckersLibrary.GraphicalImplementation;
using CheckerColor = CheckersLibrary.Color;


namespace CheckersWindows.GraphicalImplementation
{
    public class WindowsCellImplementation : CellGraphicalImplementation
    {
        private readonly PictureBox _cellBox; // image of cell for windows form

        private void InitializeCellBox()
        {
            ((ISupportInitialize)(_cellBox)).BeginInit();
            _cellBox.Name = "cellBox";
            _cellBox.Size = new Size(75, 75);
            _cellBox.SizeMode = PictureBoxSizeMode.Zoom;
            _cellBox.TabIndex = 1;
            _cellBox.TabStop = false;
            _cellBox.Click += Click;
            ((ISupportInitialize)(_cellBox)).EndInit();
        }

        public WindowsCellImplementation()
        {
            _cellBox = new PictureBox();
            InitializeCellBox();
        }
        public override void Draw(int x, int y)
        {
            _cellBox.BackColor = y % 2 + x % 2 == 1 ? Color.Gray : Color.White;
            _cellBox.Location = new Point(x*75, y*75);
        }

        public override void RemoveChecker()
        {
            var checkerBox = _cellBox.Controls[0] as PictureBox;
            if (checkerBox != null) _cellBox.Controls.Remove(checkerBox);
        }

        public override void ChancheBgColor(CheckerColor color)
        {
            switch (color)
            {
                case CheckerColor.BlueViolet:
                    _cellBox.BackColor = Color.BlueViolet;
                    break;
                case CheckerColor.Gray:
                    _cellBox.BackColor = Color.Gray;
                    break;
                default:
                    throw new ArgumentException("Unknown color");
            }
        }

        public override void AddChecker(Checker checker)
        {
            _cellBox.Controls.Add((PictureBox)checker.GetImage());
        }

        public override object GetImage()
        {
            return _cellBox;
        }
    }
}