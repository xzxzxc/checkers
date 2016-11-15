namespace checkers
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Game.StartNormalGame();

            this.SuspendLayout();
            
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 600);
            foreach (Checker chWhite in Game.chsWhite)
            {
                ((System.ComponentModel.ISupportInitialize)(chWhite.chBox)).EndInit();
                Table.tableBox.Controls.Add(chWhite.chBox);
            }
            foreach (Checker chBlack in Game.chsBlack)
            {
                Table.tableBox.Controls.Add(chBlack.chBox);
                ((System.ComponentModel.ISupportInitialize)(chBlack.chBox)).EndInit();
            }
            this.Controls.Add(Table.tableBox);
            ((System.ComponentModel.ISupportInitialize)(Table.tableBox)).EndInit();
            this.Name = "Checkers";
            this.Text = "Checkers";
            
            this.ResumeLayout(false);

        }

        #endregion
    }
}

