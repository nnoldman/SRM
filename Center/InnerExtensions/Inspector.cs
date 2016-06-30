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
    public partial class Inspector : Extension, ATrigger.ITriggerStatic
    {
        public static Inspector Instance;

        public Inspector()
        {
            Instance = this;
            InitializeComponent();
            ATrigger.DataCenter.AddInstance(this);
        }

        [ATrigger.Receiver((int)DataType.SelectObject)]
        public void OnSelect()
        {
            this.propertyGrid1.SelectedObject = Center.SelectObject.value;
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ATrigger.DataCenter.RemoveInstance(this);
            Instance = null;
        }

        [AddMenu("View(&V)/Inspector")]
        static void OnOpenView()
        {
            if (Instance == null)
            {
                Instance = new Inspector();
                Instance.TabText = "Inspector";
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
