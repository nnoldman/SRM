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
    public partial class SolutionExplorer : Extension, ATrigger.ITriggerStatic
    {
        static SolutionExplorer Instance;

        //int mIndexDir = 0;
        //int mIndexDirSel = 1;
        //int mIndexFile;

        public SolutionExplorer()
        {
            Instance = this;
            InitializeComponent();
            ATrigger.DataCenter.AddInstance(this);
            //this.fileTree.ImageList.Images.Add(Image.FromFile("./icons/ab_bottom_solid_inverse_holo.9.png"));
            //this.fileTree.ImageList.Images.Add(Image.FromFile("./icons/ab_bottom_solid_light_holo.9.png"));
            this.fileTree.NodeMouseDoubleClick += fileTree_NodeMouseDoubleClick;
            this.fileTree.KeyUp += fileTree_KeyUp;
            this.fileTree.ShowNodeToolTips = true;
            this.fileTree.ShowLines = false;
            this.fileTree.ShowPlusMinus = true;
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
            if (Center.DocumentManager.Documents.Contains(e.Node.Tag.ToString()))
            {
                Center.ActiveDocument.value = e.Node.Tag.ToString();
            }
            else if (File.Exists(e.Node.Tag.ToString()))
            {
                Center.CurrentOpenDoucment.Set(e.Node.Tag.ToString(), false);
                Center.CurrentOpenDoucment.Trigger();
            }
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            ATrigger.DataCenter.RemoveInstance(this);
            base.OnFormClosed(e);
            Instance = null;
        }

        [AddMenu("File(&F)/Select Floder")]
        static void SelectFloder()
        {
            if (Instance == null)
                CreateInstance();

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = Center.OpenFloder.value;
            var result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
                Center.OpenFloder.value = dlg.SelectedPath;
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

        [ATrigger.Receiver((int)DataType.OpenFloder)]
        void OnOpenFloder()
        {
            string floderName = Center.OpenFloder.value;

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
            var dirInfo = new DirectoryInfo(floderName);

            var files = dirInfo.GetFiles();

            var dirs = dirInfo.GetDirectories();

            foreach (var dir in dirs)
            {
                var child = new TreeNode(dir.Name);
                child.Tag = dir.FullName;
                child.ToolTipText = dir.FullName;
                parent.Nodes.Add(child);
                AddFiles(child, dir.FullName);
            }

            foreach (var f in files)
            {
                if (Center.Option.Solution.IgnoreExtensions.Contains(f.Extension))
                    continue;

                var child = new TreeNode(f.Name);
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

    }
}