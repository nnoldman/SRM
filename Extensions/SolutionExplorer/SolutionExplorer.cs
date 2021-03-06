﻿using Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SolutionExplorer
{
    [Core.ExtensionVersion(Name = "SolutionExplorer")]
    public partial class SolutionExplorer : Extension
    {
        static SolutionExplorer Instance;

        int mIndexDir = 0;
        int mIndexDirSel = 1;
        //int mIndexFile;

        public SolutionExplorer()
        {
            Instance = this;
            InitializeComponent();
            
            this.fileTree.ImageList = new ImageList();
            this.fileTree.ImageList.Images.Add(Properties.Resources.openHS);
            this.fileTree.ImageList.Images.Add(Properties.Resources.CopyHS);
            this.fileTree.NodeMouseDoubleClick += fileTree_NodeMouseDoubleClick;
            this.fileTree.KeyUp += fileTree_KeyUp;
            this.fileTree.ShowNodeToolTips = true;
            this.fileTree.ShowLines = false;
            this.fileTree.ShowPlusMinus = false;
        }

        void fileTree_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.F2)
            {
                TreeNode node = sender as TreeNode;
                if (node != null)
                    node.BeginEdit();
            }
        }

        void fileTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PortHub.ClickNode.Value = e.Node.Tag.ToString();
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            
            base.OnFormClosed(e);
            Instance = null;
        }

        [AddMenu("File(&F)/Select Floder")]
        static void SelectFloder()
        {
            if (Instance == null)
                CreateInstance();

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = Center.Option.Solution.LastSolutionPath;
            var result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                PortHub.OnOpenFloder.Value = dlg.SelectedPath;
        }

        static void CreateInstance()
        {
            Instance = new SolutionExplorer();
            Instance.TabText = "SolutionExplorer";
            Instance.Show(Center.Form.DockerContainer, DockState.Float);
        }

        [AddMenu("View(&V)/SolutionExplorer")]
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

        [Watcher((int)ID.OpenFloder)]
        void OnOpenFloder()
        {
            string floderName = PortHub.OnOpenFloder.Value;

            if (!Directory.Exists(floderName))
                return;
            Center.Option.Solution.LastSolutionPath = floderName;
            DoOpen();
        }

        void DoOpen()
        {
            if (string.IsNullOrEmpty(Center.Option.Solution.LastSolutionPath))
                return;

            this.fileTree.Nodes.Clear();

            TreeNode root = new TreeNode(Path.GetFileName(Center.Option.Solution.LastSolutionPath));
            root.Tag = Center.Option.Solution.LastSolutionPath;
            this.fileTree.Nodes.Add(root);
            AddFiles(root, Center.Option.Solution.LastSolutionPath);
        }

        void AddFiles(TreeNode parent, string floderName)
        {
            if (!Directory.Exists(floderName))
                return;

            var dirInfo = new DirectoryInfo(floderName);
     
            var files = dirInfo.GetFiles();

            var dirs = dirInfo.GetDirectories();

            foreach (var dir in dirs)
            {
                var child = new TreeNode(dir.Name);
                child.Tag = dir.FullName;
                child.ToolTipText = dir.FullName;
                child.ImageIndex = mIndexDir;
                parent.Nodes.Add(child);
                AddFiles(child, dir.FullName);
            }

            foreach (var f in files)
            {
                if (Center.Option.Solution.IgnoreExtensions.Contains(f.Extension))
                    continue;

                var child = new TreeNode(f.Name);
                child.ImageIndex = mIndexDirSel;
                child.SelectedImageIndex = mIndexDirSel;
                parent.Nodes.Add(child);
                child.Tag = f.FullName;
                child.ToolTipText = f.FullName;
            }
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            DoOpen();
        }

        private void fileTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.fileTree.SelectedNode = e.Node;
                Point pos = new Point(e.Node.Bounds.X + e.Node.Bounds.Width, e.Node.Bounds.Y + e.Node.Bounds.Height / 2);
                this.contextMenuStrip1.Show(e.Node.TreeView, pos);
            }
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = this.fileTree.SelectedNode.Tag.ToString();

            Shell.OpenFloder(path);
        }

    }
}
