using App;
using Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Plugins.PluginVersion(Name = "TextEditor", Version = 1)]

public class TextEditor : Docker, Plugins.Plugin
{
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;

    static List<TextEditor> mInstances = new List<TextEditor>();

    public TextEditor()
    {
        InitializeComponent();
        mInstances.Add(this);
    }

    public bool OnInit()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public bool OnExit()
    {
        throw new Exception("The method or operation is not implemented.");
    }
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(292, 331);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeAllToolStripMenuItem.Text = "CloseAll";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click_1);
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(292, 331);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "TextEditor";
            this.TabPageContextMenuStrip = this.contextMenuStrip1;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TextEditor_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    protected override bool IsDocument()
    {
        return true;
    }

    public override void OnDBClickNC()
    {
        TextEditor editor = new TextEditor();
        editor.TabText = "New";
        editor.Show(MainForm.docker, WeifenLuo.WinFormsUI.Docking.DockState.Document);
    }

    static void CloseAll()
    {
        foreach (var instance in mInstances)
        {
            instance.Hide();
            instance.Dispose();
        }
        mInstances.Clear();
    }

    private void TextEditor_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        mInstances.Remove(this);
    }

    private void closeAllToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        CloseAll();
    }
}
