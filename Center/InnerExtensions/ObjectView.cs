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
    public partial class ObjectView : Extension, ATrigger.ITriggerStatic
    {
        static ObjectView Instance;

        public ObjectView()
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

        [AddMenu("View(&V)/ObjectView")]
        static void OnOpenView()
        {
            if (Instance == null)
            {
                Instance = new ObjectView();
                Instance.TabText = "ObjectView";
                Instance.Show(Center.Form.DockerContainer, DockState.Float);
            }
            else
            {
                Instance.Hide();
                Instance.Dispose();
                Instance = null;
            }
        }

        [ATrigger.Receiver((int)DataType.ViewObject)]
        static void OnViewObjectChange()
        {
            Instance.treeView1.Nodes.Clear();
            CreateNode(Center.ViewObject.value, null, Instance.treeView1);
        }


        static void CreateNode(Core.Object obj, TreeNode parent,TreeView root)
        {
            TreeNode node = new TreeNode();
            node.Text = obj.Name ?? obj.GetType().Name;
            node.Tag = obj;
            if (parent != null)
                parent.Nodes.Add(node);
            else
                root.Nodes.Add(node);

            foreach (var child in obj.Children)
                CreateNode(child, node, root);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                Center.SelectObject.value = (Core.Object)e.Node.Tag;
        }
    }
}
