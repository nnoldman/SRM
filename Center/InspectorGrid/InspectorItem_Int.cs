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
    [InspectorType(typeof(int))]
    public partial class InspectorItem_Int : InspectorItem
    {
        public InspectorItem_Int()
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
            Field.SetValue(this.Target, int.Parse(this.Content.Text));
        }

        private void Content_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            //if (e.KeyChar == 46)                       //小数点      
            //{
            //    if (this.Content.Text.Length <= 0)
            //        e.Handled = true;     
            //    else
            //    {
            //        float f;
            //        if (float.TryParse(this.Content.Text + e.KeyChar.ToString(), out f))
            //        {
            //            e.Handled = false;
            //        }
            //    }
            //}
        }
    }
}
