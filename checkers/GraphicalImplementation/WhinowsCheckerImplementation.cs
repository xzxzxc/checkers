using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CheckersLibrary.Cells;
using CheckersLibrary.GraphicalImplementation;
using CheckersWindows.Properties;
using CheckerColor = CheckersLibrary.Color;
using SystemColor = System.Drawing.Color;

namespace CheckersWindows.GraphicalImplementation
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

        public WhinowsCheckerImplementation()
        {
            _checkerBox = new PictureBox();
            InitializeChBox();
        }

        public override void ChancheBgColor(CheckerColor color)
        {
            switch (color)
            {
                case CheckerColor.Khaki:
                    _checkerBox.BackColor = Color.Khaki;
                    break;
                case CheckerColor.Transparent:
                    _checkerBox.BackColor = Color.Transparent;
                    break;
                default:
                    throw new ArgumentException("Unknown color");
            }
        }

        private void InitializeChBox()
        {
            ((ISupportInitialize)(_checkerBox)).BeginInit();
            _checkerBox.BackColor = SystemColor.Transparent;
            _checkerBox.Location = new Point(0, 0);
            _checkerBox.Name = "chBox";
            _checkerBox.Size = new Size(75, 75);
            _checkerBox.SizeMode = PictureBoxSizeMode.Zoom;
            _checkerBox.TabIndex = 1;
            _checkerBox.TabStop = false;
            _checkerBox.Click += Click;
            ((ISupportInitialize)(_checkerBox)).EndInit();
        }

        public override void Draw(CheckerColor checkerColor, Cell cell)
        {
            var pictureBox = cell.GetImage() as PictureBox;
            pictureBox?.Controls.Add(_checkerBox);
            if (checkerColor == CheckerColor.Black)
            {
                _checkerBox.Image = Resources.black_checker;
                _checkerBox.Image.Tag = "black";
            }
            if (checkerColor == CheckerColor.White)
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