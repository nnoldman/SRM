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
    public partial class Output : Extension, ATrigger.ITriggerStatic
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
    }
}
