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
            string asmInput = Headerquater.Instance.InputAsm;
            string asmOutput = Headerquater.Instance.OututASM;
            string inputPortName = this.comboBox1.SelectedItem.ToString();
            PortOption option = Center.Option.GetOption<PortOption>();

            var item = option.PortPairs.Find(i => i.Target.AsmName == asmOutput && i.Target.PortName == this.Title);

            if (item == null)
            {
                item = new PortPair();
                option.PortPairs.Add(item);
            }

            item.Target.AsmName = asmOutput;
            item.Target.PortName = this.Title;
            item.Input.AsmName = asmInput;
            item.Input.PortName = inputPortName;

            Bus.ChangePortTarget(item.Target.AsmName, item.Target.PortName
                , item.Input.AsmName, item.Input.PortName);
        }
    }
}
