using Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

[Core.ExtensionVersion(Name = "SolutionExplorer")]

public class SolutionExplorer : Extension, ATrigger.ITriggerStatic
{
    private System.Windows.Forms.TreeView fileTree;

    static SolutionExplorer Instance;

    int mIndexDir = 0;
    int mIndexDirSel = 1;
    int mIndexFile;
    private System.ComponentModel.IContainer components;

    public SolutionExplorer()
    {
        InitializeComponent();
        ATrigger.DataCenter.AddInstance(this);
        //this.fileTree.ImageList.Images.Add(Image.FromFile("./icons/ab_bottom_solid_inverse_holo.9.png"));
        //this.fileTree.ImageList.Images.Add(Image.FromFile("./icons/ab_bottom_solid_light_holo.9.png"));
        this.fileTree.NodeMouseDoubleClick += fileTree_NodeMouseDoubleClick;
        this.fileTree.KeyUp += fileTree_KeyUp;
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
        if(Center.DocumentManager.Documents.Contains(e.Node.Tag.ToString()))
        {
            Center.ActiveDocument.value = e.Node.Tag.ToString();
        }
        else if (File.Exists(e.Node.Tag.ToString()))
        {
            Center.CurrentOpenDoucment.value = e.Node.Tag.ToString();
        }
    }

    protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
    {
        ATrigger.DataCenter.RemoveInstance(this);
        base.OnFormClosed(e);
        Instance = null;
    }

    [ATrigger.Receiver((int)DataType.View)]
    static void OnViewChange()
    {
        if ((Type)Center.View.Args[0] == typeof(SolutionExplorer))
        {
            if (Instance == null)
            {
                Instance = new SolutionExplorer();
                Instance.TabText = "SolutionExplorer";
                Instance.Show(Center.Container, DockState.Float);
            }
            else
            {
                Instance.Hide();
                Instance.Dispose();
                Instance = null;
            }
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
        TreeNode root = new TreeNode(Center.Option.Solution.LastSolutionPath);
        root.Tag = Center.Option.Solution.LastSolutionPath;
        this.fileTree.Nodes.Add(root);
        AddFiles(root, Center.Option.Solution.LastSolutionPath);
    }

    void AddFiles(TreeNode parent,string floderName)
    {
        var dirInfo = new DirectoryInfo(floderName);

        var files = dirInfo.GetFiles();

        var dirs = dirInfo.GetDirectories();

        foreach (var dir in dirs)
        {
            var child = new TreeNode(dir.Name);
            child.Tag = dir.FullName;
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
        }
    }
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        DoOpen();
    }
    private void InitializeComponent()
    {
            this.fileTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // fileTree
            // 
            this.fileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTree.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileTree.LabelEdit = true;
            this.fileTree.Location = new System.Drawing.Point(0, 0);
            this.fileTree.Name = "fileTree";
            this.fileTree.Size = new System.Drawing.Size(338, 486);
            this.fileTree.TabIndex = 0;
            // 
            // SolutionExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(338, 486);
            this.Controls.Add(this.fileTree);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "SolutionExplorer";
            this.ResumeLayout(false);

    }
}