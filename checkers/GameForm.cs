﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Game.StartNormalGame();
        }

    }
}
