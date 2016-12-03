using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using checkers.Cells;
using checkers.Checkers;
using checkers.Properties;

namespace checkers.GraphicalImplementation
{
    public delegate void Click();

    public abstract class CheckerGraphicalImplementation
    {
        public abstract void CheckerDraw(Color checkerColor);
        public abstract void CheckerChangePosition(Checker checker, Cell cell);
        public abstract void ChancheBgColor(Color color);
        public abstract void BeQueen();
        public abstract Object GetImage();
        public abstract bool Visible { set; }
        public event Click CheckerClick;

        protected void ChClick(Object sender, EventArgs e)
        {
            CheckerClick?.Invoke();
        }
    }

    public class WhinowsCheckerImplementation : CheckerGraphicalImplementation
    {
        private readonly PictureBox _checkerBox;

        public override void BeQueen()
        {
            if (_checkerBox.Image == Resources.black_checker)
                _checkerBox.Image = Resources.black_checker_queen;
            if (_checkerBox.Image == Resources.white_checker)
                _checkerBox.Image = Resources.white_checker_queen;
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

        public override void CheckerChangePosition(Checker checker, Cell cell)
        {
            checker.Cell.Clear();
            (cell.GetImage() as PictureBox).Controls.Add(_checkerBox);
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
            _checkerBox.Click += ChClick;
            (cell.GetImage() as PictureBox).Controls.Add(_checkerBox);
            ((ISupportInitialize)(_checkerBox)).EndInit();
        }

        public override void CheckerDraw(Color checkerColor)
        {
            if (checkerColor == Color.Black)
                _checkerBox.Image = Resources.black_checker;
            if (checkerColor == Color.White)
                _checkerBox.Image = Resources.white_checker;
        }

        public override Object GetImage()
        {
            return _checkerBox;
        }
    }

    public abstract class CellGraphicalImplementation
    {
        public abstract void CellDraw();
        public abstract void RemoveChecker();
        public abstract void ChancheBgColor(Color color);
        public abstract void AddChecker(Checker checker);
        public abstract Object GetImage();

        public event Click CellClick;
        protected Checker _checker;
        public Checker Checker => _checker;

        protected void CClick(Object sender, EventArgs e)
        {
            CellClick.Invoke();
        }
    }

    public class WindowsCellImplementation : CellGraphicalImplementation
    {
        private PictureBox _cellBox; // image of cell for windows form
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
            _cellBox.Click += CClick;
            ((ISupportInitialize)(_cellBox)).EndInit();
        }

        public WindowsCellImplementation(int x, int y)
        {
            _cellBox = new PictureBox();
            _x = x;
            _y = y;
        }
        public override void CellDraw()
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
