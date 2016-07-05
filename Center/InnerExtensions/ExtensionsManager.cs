using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using WeifenLuo.WinFormsUI.Docking;

namespace Core
{
    [Core.ExtensionVersion(Name = "Extensions")]
    public partial class ExtensionsManager : Extension, ATrigger.IStaticEmitterContainer
    {
        static ExtensionsManager Instance;

        public ExtensionsManager()
        {
            Instance = this;

            ATrigger.DataCenter.AddInstance(this);

            InitializeComponent();

            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;

            ShowContent();
        }



        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ATrigger.DataCenter.RemoveInstance(this);
            Instance = null;
        }

        [ATrigger.Receiver((int)DataType.ExtensionsLoaded)]
        public void ShowContent()
        {
            this.dataGridView1.Rows.Clear();

            foreach (var item in Center.ExtensionLoader.Types)
            {
                int cnt = this.dataGridView1.Rows.Add();
                this.dataGridView1[0, cnt].Value = item.Key;
                this.dataGridView1[1, cnt].Value = item.Value.Module;
            }
        }

        [AddMenu("View(&V)/ExtensionsManager")]
        static void OnOpenView()
        {
            if (Instance == null)
            {
                Instance = new ExtensionsManager();
                Instance.TabText = "ExtensionsManager";
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
