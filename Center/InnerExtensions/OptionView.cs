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
    public partial class OptionView : Extension, ATrigger.ITriggerStatic
    {
        static OptionView Instance;

        public OptionView()
        {
            Instance = this;
            InitializeComponent();
            ATrigger.DataCenter.AddInstance(this);

            ShowContent();
        }


        void ShowContent()
        {
            this.treeView1.Nodes.Clear();

            foreach (var child in Center.Option.Children)
            {
                TreeNode node = new TreeNode(child.Name);
                node.Tag = child;
                this.treeView1.Nodes.Add(node);
            }
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ATrigger.DataCenter.RemoveInstance(this);
            Instance = null;
        }

        [AddMenu("View(&V)/Option")]
        static void OnOpenView()
        {
            Center.SelectObject.value = Center.Option;
            //if (Instance == null)
            //{
            //    Instance = new OptionView();
            //    Instance.TabText = "OptionView";
            //    Instance.Show(Center.Form.DockerContainer, DockState.Float);
            //}
            //else
            //{
            //    Instance.Hide();
            //    Instance.Dispose();
            //    Instance = null;
            //}
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
                Center.SelectObject.value = (Object)this.treeView1.SelectedNode.Tag;
        }
    }
}
