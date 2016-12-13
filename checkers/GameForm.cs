using System;
using System.Windows.Forms;
using checkers.GraphicalImplementation;
using CheckersLibrary;
using CheckersLibrary.GraphicalImplementation;

namespace checkers
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeForm();
            InitializeMenu();
            InitializeField();
            Game.EndGame += Congratulations;
        }

        private void Congratulations(Color color)
        {
            MessageBox.Show($"{color} comand win", @"Game is end");
        }

        private void StartNewGameClick(Object sender, EventArgs e)
        {
            Game.Clear();
            Game.StartNormalGame(GenerateArrayCheckerImplementations());
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

        private CheckerGraphicalImplementation[,,] GenerateArrayCheckerImplementations()
        {
            CheckerGraphicalImplementation[,,] windowsCheckerImplementations = new CheckerGraphicalImplementation[2, 3, 4];
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        windowsCheckerImplementations[k, i, j] = new WhinowsCheckerImplementation();
                    }
                }
            }
            return windowsCheckerImplementations;
        }
    }
}
