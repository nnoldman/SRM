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
            this.IsMdiContainer = true;
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

        Plugins.Docker mEditor;

        private void texteditor_Click(object sender, EventArgs e)
        {
            if (mEditor == null || mEditor.IsDisposed)
            {
                mEditor = new Plugins.Docker();
                mEditor.Text = "Editor";
            }

            if (mEditor.Visible)
                mEditor.Hide();
            else
                mEditor.Show(this);
        }

        private void floder_Click(object sender, EventArgs e)
        {

        }


    }
}
