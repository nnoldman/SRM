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

        const int ColumnName = 0;
        const int ColumnComplier = 1;
        const int ColumnLinker = 2;
        const int ColumnDebugger = 3;

        public DebuggerConfiguration()
        {
            Instance = this;
            InitializeComponent();
            ATrigger.DataCenter.AddInstance(this);
            
            Initialize();
        }

        void comboBox_BuildMode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Center.Option.BuildOption.CurrentMode = this.comboBox_Builder.SelectedIndex == 0 ?
                BuildMode.Debug : BuildMode.Release;
        }

        void comboBox_Builder_SelectedIndexChanged(object sender, EventArgs e)
        {
            Center.Option.BuildOption.CurrentBuilderName = this.comboBox_Builder.Text;
        }

        void Initialize()
        {
            LoadOption();
            InitBaseInfo();
        }
        void InitBaseInfo()
        {
            this.comboBox_Builder.Items.Clear();

            foreach (var b in Center.Option.BuildOption.Builders)
                this.comboBox_Builder.Items.Add(b.Name);

            this.comboBox_Builder.SelectedIndex = Center.Option.BuildOption.Builders.FindIndex(
                (item) => item.Name == Center.Option.BuildOption.CurrentBuilderName);


            this.comboBox_BuildMode.Items.Add(BuildMode.Debug.ToString());
            this.comboBox_BuildMode.Items.Add(BuildMode.Release.ToString());

            this.comboBox_BuildMode.SelectedIndex = Center.Option.BuildOption.CurrentMode == BuildMode.Debug ?
                0 : 1;
        }
        public void LoadOption()
        {
            this.dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;
            
            this.dataGridView1.Rows.Clear();

            foreach (var b in Center.Option.BuildOption.Builders)
            {
                int cnt = this.dataGridView1.Rows.Add();
                this.dataGridView1[ColumnName, cnt].Value = b.Name;
                this.dataGridView1[ColumnComplier, cnt].Value = b.Complier;
                this.dataGridView1[ColumnLinker, cnt].Value = b.Linker;
                this.dataGridView1[ColumnDebugger, cnt].Value = b.Debugger;
            }

            this.dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
        }
        void SaveOption()
        {
            Center.Option.BuildOption.Builders.Clear();

            for (int i = 0; i < this.dataGridView1.RowCount - 1; ++i)
            {
                Builder b = new Builder();
                b.Name = this.dataGridView1[ColumnName, i].Value.ToString();
                b.Complier = this.dataGridView1[ColumnComplier, i].Value.ToString();
                b.Linker = this.dataGridView1[ColumnLinker, i].Value.ToString();
                b.Debugger = this.dataGridView1[ColumnDebugger, i].Value.ToString();
                Center.Option.BuildOption.Builders.Add(b);
            }
        }

        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SaveOption();
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
