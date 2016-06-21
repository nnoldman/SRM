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

    public TextEditor()
    {
        InitializeComponent();
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
        this.richTextBox1 = new System.Windows.Forms.RichTextBox();
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
        // TextEditor
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.ClientSize = new System.Drawing.Size(292, 331);
        this.Controls.Add(this.richTextBox1);
        this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        this.Name = "TextEditor";
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
}
