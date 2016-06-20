using Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitViewMenus();
        }

        public void InitViewMenus()
        {
            string[] menus = new string[] {
                "FFF","GGG","HHH"
            };
            foreach (var menu in menus)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = menu;
                item.Click += OnViewClick;
                this.ViewToolStripMenuItem.DropDownItems.Add(item);
            }
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

        private void OnViewClick(object sender, EventArgs e)
        {
            Docker.Toggler(sender.ToString(), this.dockPanel1);
        }

        private void floder_Click(object sender, EventArgs e)
        {

        }
    }
}
