using System;
using System.Windows.Forms;

namespace checkers
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeForm();
            InitializeMenu();
            InitializeField();
        }

        private void StartNewGameClick(Object sender, EventArgs e)
        {
            Game.Clear();
            Game.StartNormalGame();
        }

        private void ExitClick(Object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UndoClick(Object sender, EventArgs e)
        {
            Game.Undo();
        }

        private void RedoClick(Object sender, EventArgs e)
        {
            Game.Redo();
        }
    }
}
