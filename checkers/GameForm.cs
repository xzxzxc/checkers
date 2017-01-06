using System;
using System.Drawing;
using System.Windows.Forms;
using CheckersLibrary;
using CheckersLibrary.GraphicalImplementation;
using CheckersWindows.GraphicalImplementation;
using Color = CheckersLibrary.Color;

namespace CheckersWindows
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
            AskForStartupSettings();
            
        }

        private void AskForStartupSettings()
        {
            Form startupSettingsForm = new Form();

            GroupBox colorGroupBox = new GroupBox();

            RadioButton whiteColorRadioButton = new RadioButton();
            whiteColorRadioButton.Text = @"White";
            whiteColorRadioButton.Location = new Point(10, 15);
            RadioButton blackColorRadioButton = new RadioButton();
            blackColorRadioButton.Text = @"Black";
            blackColorRadioButton.Location = new Point(whiteColorRadioButton.Location.X, whiteColorRadioButton.Location.Y
                + whiteColorRadioButton.Height + 5);
            whiteColorRadioButton.Checked = true;

            colorGroupBox.Text = @"Select your color";
            colorGroupBox.FlatStyle = FlatStyle.Flat;
            colorGroupBox.Location = new Point(10, 10);
            colorGroupBox.Height = whiteColorRadioButton.Height + blackColorRadioButton.Height + 25;
            colorGroupBox.Controls.Add(whiteColorRadioButton);
            colorGroupBox.Controls.Add(blackColorRadioButton);

            CheckBox vsCpuCheckBox = new CheckBox();
            vsCpuCheckBox.Text = @"Vs CPU";
            vsCpuCheckBox.Location = new Point(10, colorGroupBox.Location.Y + colorGroupBox.Height + 10);

            Button acceptButton = new Button();
            acceptButton.Text = @"Ok";
            acceptButton.Location = new Point(10, vsCpuCheckBox.Location.Y + vsCpuCheckBox.Height + 10);
            acceptButton.Click += (sender, args) =>
            {
                Color color;
                if (whiteColorRadioButton.Checked)
                    color = Color.White;
                else if (blackColorRadioButton.Checked)
                    color = Color.Black;
                else
                    throw new ArgumentOutOfRangeException();
                Game.StartNormalGame(color, GenerateArrayCheckerImplementations(), vsCpuCheckBox.Checked);
                startupSettingsForm.Close();
            };

            // Set the caption bar text of the form.   
            startupSettingsForm.Text = @"Select initial settings";
            
            // Define the border style of the form to a dialog box.
            startupSettingsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            startupSettingsForm.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            startupSettingsForm.MinimizeBox = false;
            // Set the accept button of the form to acceptButton.
            startupSettingsForm.AcceptButton = acceptButton;
            
            // Set the start position of the form to the center of the screen.
            startupSettingsForm.StartPosition = FormStartPosition.CenterScreen;
            startupSettingsForm.Height = acceptButton.Location.Y + acceptButton.Height + 50;

            startupSettingsForm.Controls.Add(colorGroupBox);
            startupSettingsForm.Controls.Add(vsCpuCheckBox);
            startupSettingsForm.Controls.Add(acceptButton);

            // Display the form as a modal dialog box.
            startupSettingsForm.ShowDialog();
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GameForm";
            this.ResumeLayout(false);

        }
    }
}
