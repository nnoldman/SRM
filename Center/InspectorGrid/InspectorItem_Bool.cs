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
    [InspectorType(typeof(bool))]
    public partial class InspectorItem_Bool : InspectorItem
    {
        public InspectorItem_Bool()
        {
            InitializeComponent();
        }
        void SetValue()
        {
            object f = Field.GetValue(this.Target);
            if (f != null)
                this.checkBox1.CheckState = ((bool)f) ? CheckState.Checked : CheckState.Unchecked;
        }
        public override void OnInit()
        {
            this.checkBox1.Text = Field.Name;
            SetValue();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Field.SetValue(this.Target, this.checkBox1.CheckState == CheckState.Checked);
        }
    }
}
