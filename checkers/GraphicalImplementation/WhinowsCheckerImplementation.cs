using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using checkers.Cells;
using checkers.Checkers;
using checkers.Properties;

namespace checkers.GraphicalImplementation
{
    public class WhinowsCheckerImplementation : CheckerGraphicalImplementation
    {
        private readonly PictureBox _checkerBox;

        public override void BeQueen()
        {
            if ((string) _checkerBox.Image.Tag == "black")
                _checkerBox.Image = Resources.black_checker_queen;
            if ((string)_checkerBox.Image.Tag == "white")
                _checkerBox.Image = Resources.white_checker_queen;
        }

        public override void UnBeQueen()
        {
            if ((string)_checkerBox.Image.Tag == "black")
                _checkerBox.Image = Resources.black_checker;
            if ((string)_checkerBox.Image.Tag == "white")
                _checkerBox.Image = Resources.white_checker;
        }
        public override bool Visible
        {
            set { _checkerBox.Visible = value; }
        }

        public WhinowsCheckerImplementation(Cell cell)
        {
            _checkerBox = new PictureBox();
            InitializeChBox(cell);
        }

        public override void ChancheBgColor(Color color)
        {
            _checkerBox.BackColor = color;
        }

        private void InitializeChBox(Cell cell)
        {
            ((ISupportInitialize)(_checkerBox)).BeginInit();
            _checkerBox.BackColor = Color.Transparent;
            _checkerBox.Location = new Point(0, 0);
            _checkerBox.Name = "chBox";
            _checkerBox.Size = new Size(75, 75);
            _checkerBox.SizeMode = PictureBoxSizeMode.Zoom;
            _checkerBox.TabIndex = 1;
            _checkerBox.TabStop = false;
            _checkerBox.Click += Click;
            (cell.GetImage() as PictureBox).Controls.Add(_checkerBox);
            ((ISupportInitialize)(_checkerBox)).EndInit();
        }

        public override void Draw(Color checkerColor)
        {
            if (checkerColor == Color.Black)
            {
                _checkerBox.Image = Resources.black_checker;
                _checkerBox.Image.Tag = "black";
            }
            if (checkerColor == Color.White)
            {
                _checkerBox.Image = Resources.white_checker;
                _checkerBox.Image.Tag = "white";
            }
        }

        public override Object GetImage()
        {
            return _checkerBox;
        }
    }
}