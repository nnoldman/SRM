using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugins
{
    public class Docker : Form
    {
        static List<Docker> dockers = new List<Docker>();

        public Docker()
        {
            dockers.Add(this);
        }

        ~Docker()
        {
            dockers.Remove(this);
        }

        private List<Docker> sublings = new List<Docker>();

        public void Show(Form parent)
        {
            this.MdiParent = parent;
            this.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
