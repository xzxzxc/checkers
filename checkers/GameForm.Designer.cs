using System.Drawing;
using System.Windows.Forms;
using checkers.Cells;

namespace checkers
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Field picture box
        /// </summary>
        private PictureBox _fieldBox;
        private ToolStrip _menuToolStrip;
        private ToolStripDropDownButton _gameSessionToolStripDropDownButton;
        private ToolStripMenuItem _startNewGameToolStripMenuItem;
        private ToolStripMenuItem _saveGameToolStripMenuItem;
        private ToolStripMenuItem _loadGameToolStripMenuItem;
        private ToolStripMenuItem _exitToolStripMenuItem;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripButton _undoToolStripButton;
        private ToolStripButton _redoToolStripButton;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeMenu()
        {
            this.SuspendLayout();

            this._gameSessionToolStripDropDownButton = new ToolStripDropDownButton();
            this._gameSessionToolStripDropDownButton.Text = "Game menu";

            this._startNewGameToolStripMenuItem = new ToolStripMenuItem();
            this._startNewGameToolStripMenuItem.Name = "startNewToolStripMenuItem";
            this._startNewGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._startNewGameToolStripMenuItem.Text = "StartNew";
            this._startNewGameToolStripMenuItem.Click += StartNewGameClick;

            this._saveGameToolStripMenuItem = new ToolStripMenuItem();
            this._saveGameToolStripMenuItem.Name = "saveToolStripMenuItem";
            this._saveGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this._saveGameToolStripMenuItem.Text = "Save";

            this._loadGameToolStripMenuItem = new ToolStripMenuItem();
            this._loadGameToolStripMenuItem.Name = "loadToolStripMenuItem";
            this._loadGameToolStripMenuItem.Size = new Size(152, 22);
            this._loadGameToolStripMenuItem.Text = "Load";

            this._exitToolStripMenuItem = new ToolStripMenuItem();
            this._exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this._exitToolStripMenuItem.Size = new Size(152, 22);
            this._exitToolStripMenuItem.Text = "Exit";
            this._exitToolStripMenuItem.Click += ExitClick;

            this._toolStripSeparator1 = new ToolStripSeparator();
            this._toolStripSeparator1.Name = "toolStripSeparator1";
            this._toolStripSeparator1.Size = new Size(6, 25);

            this._redoToolStripButton = new ToolStripButton();
            this._redoToolStripButton.Name = "RedoToolStripButton";
            this._redoToolStripButton.Size = new Size(23, 22);
            this._redoToolStripButton.Text = "Redo";
            this._redoToolStripButton.Click += RedoClick;

            this._undoToolStripButton = new ToolStripButton();
            this._undoToolStripButton.Name = "UndoToolStripButton";
            this._undoToolStripButton.Size = new Size(23, 22);
            this._undoToolStripButton.Text = "Undo";
            this._undoToolStripButton.Click += UndoClick;

            _gameSessionToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[]
            {
                this._startNewGameToolStripMenuItem,
                this._saveGameToolStripMenuItem,
                this._loadGameToolStripMenuItem,
                this._exitToolStripMenuItem
            });
            this._menuToolStrip = new ToolStrip(
                new ToolStripItem[]
                {
                    _gameSessionToolStripDropDownButton,
                    _toolStripSeparator1,
                    _undoToolStripButton,
                    _redoToolStripButton
                });
            this._menuToolStrip.Location = new Point(0, 0);
            this._menuToolStrip.Name = "MenuToolStrip";
            this._menuToolStrip.Size = new Size(600, 25);
            this._menuToolStrip.TabIndex = 0;

            this.Controls.Add(this._menuToolStrip);

            this.ResumeLayout(false);
        }

        private void InitializeForm()
        {
            this.SuspendLayout();

            this.Name = "Checkers";
            this.Text = "Checkers";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 625);

            this.ResumeLayout(false);
        }

        private void InitializeField()
        {
            this.SuspendLayout();

            this._fieldBox = new PictureBox();
            this._fieldBox.Location = new Point(0, 25);
            this._fieldBox.Name = "Field";
            this._fieldBox.Size = new Size(600, 600);
            this.Controls.Add(this._fieldBox);

            for (byte i = 0; i < 8; i++)
                for (byte j = 0; j < 8; j++)
                    this._fieldBox.Controls.Add((PictureBox)TableCells.Cell[i, j].GetImage());
            
            this.ResumeLayout(false);

        }
    }
}

