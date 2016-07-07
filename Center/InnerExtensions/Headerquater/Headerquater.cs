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

namespace Core
{
    public partial class Headerquater : Extension
    {
        static Headerquater Instance;

        List<HeaderquaterLinker> mLinkers = new List<HeaderquaterLinker>();

        public Headerquater()
        {
            Instance = this;
            InitializeComponent();
            InitList(listBox1, string.Empty);
        }

        public void InitList(ListBox view, string except)
        {
            view.Items.Clear();

            var components = Bus.Components;

            foreach(var com in components)
            {
                if (!string.IsNullOrEmpty(except) && except == com.Key.ManifestModule.Name)
                    continue;
                view.Items.Add(com.Key.ManifestModule.Name);
            }
        }

        [AddMenu("Headerquater(&H)")]
        static void OnOpenView()
        {
            if (Instance == null)
            {
                CreateInstance();
            }
            else
            {
                Instance.Hide();
                Instance.Dispose();
                Instance = null;
            }
        }

        static void CreateInstance()
        {
            Instance = new Headerquater();
            Instance.TabText = "Headerquater";
            Instance.Show(Center.Form.DockerContainer, DockState.Float);
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Instance = null;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitList(this.listBox2, this.listBox1.SelectedItem.ToString());
            InitOutPorts();
        }

        void InitOutPorts()
        {
            var com = Bus.Components.Keys.First((item) => item.ManifestModule.Name == this.listBox1.SelectedItem.ToString());

            Constructure constructure;

            if(Bus.Components.TryGetValue(com, out constructure))
            {
                this.portPanel.Controls.Clear();
                mLinkers.Clear();

                foreach(var outputPort in constructure.Outputs)
                {
                    HeaderquaterLinker linker = new HeaderquaterLinker();
                    linker.Title = outputPort.field.Name;
                    this.portPanel.Controls.Add(linker);
                    linker.Dock = DockStyle.Top;

                    mLinkers.Add(linker);
                }
            }
        }
        void InitInputPorts()
        {
            var com = Bus.Components.Keys.First((item) => item.ManifestModule.Name == this.listBox2.SelectedItem.ToString());

            Constructure constructure;

            if (Bus.Components.TryGetValue(com, out constructure))
            {
                List<string> stringlist = new List<string>();
                foreach (var inputPort in constructure.Inputs)
                    stringlist.Add(inputPort.field.Name);

                foreach (var linker in mLinkers)
                    linker.SelectList = stringlist.ToArray();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitInputPorts();
        }
    }
}
