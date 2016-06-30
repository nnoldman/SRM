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
    [InspectorType(typeof(string))]
    public partial class InspectorItem_String : InspectorItem
    {
        public InspectorItem_String()
        {
            InitializeComponent();
        }
        public override void OnInit()
        {
            this.Title.Text = Field.Name;
            object f = Field.GetValue(this.Target);
            if (f != null)
                this.Content.Text = f.ToString();
        }
        private void Content_TextChanged(object sender, EventArgs e)
        {
            Field.SetValue(this.Target, this.Content.Text);
        }
    }
}
