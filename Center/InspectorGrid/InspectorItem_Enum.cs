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
    [InspectorType(typeof(Enum))]
    public partial class InspectorItem_Enum : InspectorItem
    {
        public InspectorItem_Enum()
        {
            InitializeComponent();
        }
        public override void OnInit()
        {
            this.Title.Text = Field.Name;
            object f = Field.GetValue(this.Target);

            var names = Enum.GetNames(Field.FieldType);
            foreach (var name in names)
                this.comboBox1.Items.Add(name);

            this.comboBox1.Text = f.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Field.SetValue(this.Target, Enum.Parse(this.Field.FieldType, this.comboBox1.Text));
        }
    }
}
