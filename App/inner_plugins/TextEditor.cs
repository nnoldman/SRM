using App;
using Plugins;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

[Plugins.PluginVersion(Name = "TextEditor", Version = 1)]

public class TextEditor : Docker, Plugins.Plugin
{
    public static Color GlobalBackColor = Color.FromArgb(255, 220, 230, 255);

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private ScintillaNET.Scintilla scintilla1;

    static List<TextEditor> mInstances = new List<TextEditor>();

    public new string Text
    {
        get
        {
            return this.scintilla1.Text;
        }
        set
        {
            this.scintilla1.Text = value;
        }
    }

    protected string mFileName;

    public string FileName
    {
        get
        {
            return mFileName;
        }
        set
        {
            TabText = Path.GetFileName(value);
            mFileName = value;
            if (File.Exists(mFileName))
                this.Text = File.ReadAllText(mFileName);
        }
    }

    public TextEditor()
    {
        InitializeComponent();

        this.BackColor = GlobalBackColor;

        this.scintilla1.StyleResetDefault();
        this.scintilla1.SetWhitespaceBackColor(false, GlobalBackColor);
        this.scintilla1.SetWhitespaceForeColor(false, GlobalBackColor);

        this.scintilla1.EdgeColor = GlobalBackColor;
        this.scintilla1.EdgeMode = ScintillaNET.EdgeMode.Background;
        this.scintilla1.CaretLineBackColor = GlobalBackColor;
        this.scintilla1.AdditionalCaretForeColor = GlobalBackColor;
        this.scintilla1.AdditionalCaretsVisible = false;
        this.scintilla1.Document

        this.scintilla1.Margins[1].Type = ScintillaNET.MarginType.Number;
        this.scintilla1.Margins[1].Width = 45;

        this.scintilla1.Styles[Style.LineNumber].Size = 9;
        this.scintilla1.Styles[Style.LineNumber].BackColor = GlobalBackColor;

        this.scintilla1.Styles[Style.Default].Font = "courier new";
        this.scintilla1.Styles[Style.Default].Size = 14;
        this.scintilla1.Styles[Style.Default].BackColor = GlobalBackColor;


        //this.scintilla1.Styles[Style.Lua.Default].ForeColor = Color.Black;
        //this.scintilla1.Styles[Style.Lua.Comment].ForeColor = Color.FromArgb(0, 128, 0);
        //this.scintilla1.Styles[Style.Lua.CommentLine].ForeColor = Color.FromArgb(0, 128, 0);
        //this.scintilla1.Styles[Style.Lua.Number].ForeColor = Color.Olive;
        //this.scintilla1.Styles[Style.Lua.Word].ForeColor = Color.Blue;
        //this.scintilla1.Styles[Style.Lua.Word2].ForeColor = Color.Blue;
        //this.scintilla1.Styles[Style.Lua.String].ForeColor = Color.FromArgb(163, 21, 21);
        //this.scintilla1.Styles[Style.Lua.Character].ForeColor = Color.FromArgb(163, 21, 21);
        //this.scintilla1.Styles[Style.Lua.StringEol].BackColor = Color.Pink;
        //this.scintilla1.Styles[Style.Lua.Operator].ForeColor = Color.Purple;
        //this.scintilla1.Styles[Style.Lua.Preprocessor].ForeColor = Color.Maroon;

        this.scintilla1.Lexer = Lexer.Cpp;

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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scintilla1 = new ScintillaNET.Scintilla();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.closeAllToolStripMenuItem.Text = "CloseAll";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click_1);
            // 
            // scintilla1
            // 
            this.scintilla1.AdditionalCaretsVisible = false;
            this.scintilla1.AllowDrop = true;
            this.scintilla1.CaretLineBackColor = System.Drawing.Color.Wheat;
            this.scintilla1.CaretLineVisible = true;
            this.scintilla1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla1.EdgeColor = System.Drawing.Color.LightGray;
            this.scintilla1.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.scintilla1.Lexer = ScintillaNET.Lexer.Cpp;
            this.scintilla1.Location = new System.Drawing.Point(0, 0);
            this.scintilla1.Margin = new System.Windows.Forms.Padding(5);
            this.scintilla1.Margins.Left = 3;
            this.scintilla1.Margins.Right = 3;
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Size = new System.Drawing.Size(292, 331);
            this.scintilla1.TabIndex = 1;
            this.scintilla1.Text = "scintilla1";
            this.scintilla1.UseTabs = true;
            this.scintilla1.DragDrop += new System.Windows.Forms.DragEventHandler(this.scintilla1_DragDrop);
            this.scintilla1.DragEnter += new System.Windows.Forms.DragEventHandler(this.scintilla1_DragEnter);
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(292, 331);
            this.Controls.Add(this.scintilla1);
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

    public static void CreateDocument(string path)
    {
        TextEditor editor = new TextEditor();
        editor.FileName = path;
        editor.Show(MainForm.docker, WeifenLuo.WinFormsUI.Docking.DockState.Document);
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
    protected override string GetPersistString()
    {
        return string.Format("Type={0};TabText={1}", GetType().Name, FileName);
    }
    public override void LoadFromPersistString(PersistStringParser parser)
    {
        FileName = parser["TabText"];
    }

    private void scintilla1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            e.Effect = DragDropEffects.All;
    }

    private void scintilla1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (string file in files)
            TextEditor.CreateDocument(file);
    }
}
