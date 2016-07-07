using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public partial class HeaderquaterLinker : UserControl
    {
        public HeaderquaterLinker()
        {
            InitializeComponent();
        }
        public string Title
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }
        public string[] SelectList
        {
            set { this.comboBox1.Items.AddRange(value); }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
