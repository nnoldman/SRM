using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void CloseFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void texteditor_Click(object sender, EventArgs e)
        {
            Plugins.Docker docker = new Plugins.Docker();
            docker.Text = "XXX";
            docker.BackColor = Color.Gray;
            docker.Dock = DockStyle.Fill;
            docker.Show();
        }

        private void floder_Click(object sender, EventArgs e)
        {

        }


    }
}
