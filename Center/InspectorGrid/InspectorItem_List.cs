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
    //[InspectorType(typeof(List<>))]
    public partial class InspectorItem_List : InspectorItem
    {
        public InspectorItem_List()
        {
            InitializeComponent();
        }
        public override void OnInit()
        {
            this.Title.Text = Field.Name;
            object f = Field.GetValue(this.Target);
        }
        private void Content_TextChanged(object sender, EventArgs e)
        {
            //Field.SetValue(this.Target, bool.Parse(this.Content.Text));
        }
    }
}
