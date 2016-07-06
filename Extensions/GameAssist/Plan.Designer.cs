namespace GameAssist
{
    public partial class Plan
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fileTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileTree
            // 
            this.fileTree.ContextMenuStrip = this.contextMenuStrip1;
            this.fileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTree.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileTree.LabelEdit = true;
            this.fileTree.Location = new System.Drawing.Point(0, 0);
            this.fileTree.Name = "fileTree";
            this.fileTree.Size = new System.Drawing.Size(403, 486);
            this.fileTree.TabIndex = 0;
            this.fileTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileTree_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInExplorerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 48);
            // 
            // openInExplorerToolStripMenuItem
            // 
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.openInExplorerToolStripMenuItem.Text = "OpenInExplorer";
            this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInExplorerToolStripMenuItem_Click);
            // 
            // SolutionExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(403, 486);
            this.Controls.Add(this.fileTree);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "SolutionExplorer";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView fileTree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
    }
}
