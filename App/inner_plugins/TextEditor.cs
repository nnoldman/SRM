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
            this.Click += new System.EventHandler(this.OnClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this.ResumeLayout(false);

    }

    protected override bool IsDocument()
    {
        return true;
    }

    private void OnClick(object sender, EventArgs e)
    {

    }

    private void OnMouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
    {

    }
    protected override void WndProc(ref System.Windows.Forms.Message m)
    {
        if (m.Msg == 533)
        {
            //if (this.PanelPane.DisplayRectangle.Contains(MousePosition))
            if (this.Pane.DisplayRectangle.Contains(MousePosition))
            {
                Debug.WriteLine(MousePosition);
            }
        }
        base.WndProc(ref m);
    }
}
