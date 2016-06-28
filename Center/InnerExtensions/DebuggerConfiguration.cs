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
    public partial class DebuggerConfiguration : Extension, ATrigger.ITriggerStatic
    {
        static DebuggerConfiguration Instance;

        public DebuggerConfiguration()
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

        [AddMenu("View(&V)/DebuggerConfiguration")]
        static void OnOpenView()
        {
            if (Instance == null)
            {
                Instance = new DebuggerConfiguration();
                Instance.TabText = "DebuggerConfiguration";
                Instance.Show(Center.Form.DockerContainer, DockState.Float);
            }
            else
            {
                Instance.Hide();
                Instance.Dispose();
                Instance = null;
            }
        }

        [AddShortCut(ShortCutIndex.Build, Shortcut.Modifiers.Control | Shortcut.Modifiers.Shift, Keys.B, "Build")]
        static void Build()
        {
            Builder builder = Center.Option.BuildOption.CurrentBuilder;
            if (!builder)
                return;
            builder.Build();
        }

        [AddShortCut(ShortCutIndex.Run, Shortcut.Modifiers.None, Keys.F5, "Run")]
        static void Run()
        {
        }

        [AddShortCut(ShortCutIndex.Stop, Shortcut.Modifiers.Shift, Keys.F5, "Stop")]
        static void Stop()
        {
        }
    }
}
