using Core;
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
    [Core.ExtensionVersion(Name = "ShortCutSetting")]

    public partial class ShortCutSetting : Extension, ATrigger.IStaticEmitterContainer
    {
        static ShortCutSetting Instance;

        public ShortCutSetting()
        {
            Instance = this;
            ATrigger.DataCenter.AddInstance(this);
            InitializeComponent();
            ShowContent();
        }

        static void CreateInstance()
        {
            Instance = new ShortCutSetting();
            Instance.TabText = "ShortCutSetting";
            Instance.Show(Center.Form.DockerContainer, DockState.Float);
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            ATrigger.DataCenter.RemoveInstance(this);
            base.OnFormClosed(e);
            Instance = null;
        }

        [AddMenu("View(&V)/ShortCutSetting")]
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

        [ATrigger.Receiver((int)DataType.ExtensionsLoaded)]
        public void ShowContent()
        {
            this.dataGridView1.Rows.Clear();

            foreach (var item in Center.HotKeys)
            {
                int cnt = this.dataGridView1.Rows.Add();
                this.dataGridView1[0, cnt].Value = item.Value.Text;
                this.dataGridView1[1, cnt].Value = item.Value.DefaultModifiers.ToString();
                this.dataGridView1[2, cnt].Value = item.Value.DefaultKey.ToString();
            }
        }
    }
}
