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
    [InspectorType(typeof(float))]
    public partial class InspectorItem_Float : InspectorItem
    {
        public InspectorItem_Float()
        {
            InitializeComponent();
        }
        public override void OnInit()
        {
            this.Title.Text = Field.Name;
            this.Content.Text = Field.GetValue(this.Target).ToString();
        }
        private void Content_TextChanged(object sender, EventArgs e)
        {
            Field.SetValue(this.Target, float.Parse(this.Content.Text));
        }

        const char Dot = '.';

        private void Content_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == Dot)
            {
                if (!this.Content.Text.Contains(Dot) && this.Content.Text.Length > 0)
                    e.Handled = false;
            }
        }
    }
}
