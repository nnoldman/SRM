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

    public partial class ShortCutSetting : Core.Extension, ATrigger.ITriggerStatic
    {
        static ShortCutSetting Instance;

        public ShortCutSetting()
        {
            Instance = this;
            ATrigger.DataCenter.AddInstance(this);
            InitializeComponent();
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
    }
}
