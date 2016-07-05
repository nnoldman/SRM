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
    public partial class Output : Extension, ATrigger.IStaticEmitterContainer
    {
        static Output Instance;

        public Output()
        {
            Instance = this;
            InitializeComponent();
            ATrigger.DataCenter.AddInstance(this);
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ATrigger.DataCenter.RemoveInstance(this);
            Instance = null;
        }

        [ATrigger.Receiver((int)DataType.ConsoleClear)]
        void ClearConsole()
        {
            this.textBox1.Clear();
        }

        delegate void SetTextCallback(string text);

        [ATrigger.Receiver((int)DataType.Console)]
        void WriteConsole(string text)
        {
            SetText(text);
        }

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text += text + "\r\n";
            }
        }

        [AddMenu("View(&V)/Output")]
        static void OnOpenView()
        {
            if (Instance == null)
            {
                Instance = new Output();
                Instance.TabText = "Output";
                Instance.Show(Center.Form.DockerContainer, DockState.Float);
            }
            else
            {
                Instance.Hide();
                Instance.Dispose();
                Instance = null;
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(this.textBox1, e.X + 15, e.Y - 10);
            }
        }
    }
}
