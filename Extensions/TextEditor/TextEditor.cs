using Core;
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


[Core.PluginVersion(Name = "TextEditor", Version = 1)]

public class TextEditor : Docker, Core.Extension
{
    static Dictionary<string, TextEditor> mDocuments = new Dictionary<string, TextEditor>();
    static public TextEditor CreateDocument(string file)
    {
        TextEditor editor = null;
        if (editor != null)
            editor.Show(Center.Container, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        return editor;
    }
    public static Color GlobalBackColor = Color.FromArgb(0, 220, 230, 255);

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
            TabText = string.IsNullOrEmpty(value) ? "New" : Path.GetFileName(value);
            mFileName = value;
            if (File.Exists(mFileName))
                this.Text = File.ReadAllText(mFileName);
        }
    }

    public TextEditor()
    {
        InitializeComponent();

        this.scintilla1.StyleResetDefault();
        this.scintilla1.SetWhitespaceBackColor(true, GlobalBackColor);
        this.scintilla1.SetWhitespaceForeColor(true, GlobalBackColor);

        this.scintilla1.EdgeColor = GlobalBackColor;
        this.scintilla1.EdgeMode = ScintillaNET.EdgeMode.Background;
        this.scintilla1.CaretLineBackColor = GlobalBackColor;
        this.scintilla1.AdditionalCaretForeColor = GlobalBackColor;
        this.scintilla1.AdditionalCaretsVisible = false;

        this.scintilla1.Margins[1].Type = ScintillaNET.MarginType.Number;
        this.scintilla1.Margins[1].Width = 60;

        this.scintilla1.Styles[Style.Default].Font = "courier new";
        this.scintilla1.Styles[Style.Default].Size = 14;
        this.scintilla1.Styles[Style.Default].BackColor = GlobalBackColor;
        this.scintilla1.StyleClearAll();

        //this.BackgroundImage = Image.FromFile("ExampleWatermark.jpg");
        //this.BackgroundImageLayout = ImageLayout.Stretch;

        this.scintilla1.Styles[Style.Cpp.Default].ForeColor = Color.Blue;
        this.scintilla1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0);
        this.scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0);
        this.scintilla1.Styles[Style.Cpp.Number].ForeColor = Color.Black;
        this.scintilla1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
        this.scintilla1.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
        this.scintilla1.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21);
        this.scintilla1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21);
        this.scintilla1.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
        this.scintilla1.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
        this.scintilla1.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
        this.scintilla1.Styles[Style.Cpp.UserLiteral].ForeColor = Color.Maroon;
        this.scintilla1.Styles[Style.Cpp.Identifier].ForeColor = Color.FromArgb(255, 0, 0, 130);
        this.scintilla1.Styles[Style.Cpp.Identifier].Bold = false;
        
        this.scintilla1.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
        this.scintilla1.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
        this.scintilla1.Lexer = Lexer.Cpp;
        this.scintilla1.CharAdded += CharAdded;
        this.scintilla1.Delete += CharDelete;

        this.scintilla1.AutoCIgnoreCase = true;

        this.scintilla1.SetProperty("fold", "1");
        this.scintilla1.SetProperty("fold.compact", "1");

        // Configure a margin to display folding symbols
        this.scintilla1.Margins[2].Type = MarginType.Symbol;
        this.scintilla1.Margins[2].Mask = Marker.MaskAll;
        this.scintilla1.Margins[2].Sensitive = true;
        this.scintilla1.Margins[2].Width = 16;

        // Set colors for all folding markers
        for (int i = 25; i <= 31; i++)
        {
            this.scintilla1.Markers[i].SetForeColor(SystemColors.ControlLightLight);
            this.scintilla1.Markers[i].SetBackColor(SystemColors.ControlDark);
        }

        // Configure folding markers with respective symbols
        this.scintilla1.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
        this.scintilla1.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
        this.scintilla1.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
        this.scintilla1.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
        this.scintilla1.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
        this.scintilla1.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
        this.scintilla1.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

        // Enable automatic folding
        this.scintilla1.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        this.scintilla1.AssignCmdKey(Keys.Control | Keys.S, Command.Home);

        Bitmap bmp = (Bitmap)Bitmap.FromFile("vol4.bmp");
        this.scintilla1.RegisterRgbaImage(-1, bmp);

        mInstances.Add(this);
    }

    public static void SaveFile()
    {

    }

    SortedSet<string> GetCacheWords(int startPos,int len)
    {
        char ch = '\0';
        SortedSet<string> words = new SortedSet<string>();
        string text = Text;
        int allLen = text.Length;

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < allLen; ++i)
        {
            ch = text[i];

            if (i == startPos)
            {
                goto MakeWord;
            }
            else if (i > startPos && i <= startPos + len)
            {
                continue;
            }
            else if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9') || ch == '_')
            {
                sb.Append(ch);
                continue;
            }
        MakeWord:
            if (sb.Length > 0)
            {
                string word = sb.ToString();
                if (!words.Contains(word))
                    words.Add(word);
                sb.Clear();
            }
        }
        return words;
    }
    bool Match(string word, string target)
    {
        if (target.Length > word.Length)
            return false;

        int len = Math.Min(word.Length, target.Length);
        
        char ch1, ch2;
        
        for (int i = 0; i < len; ++i)
        {
            ch1 = word[i];
            ch2 = target[i];
            if (ch1 == ch2 || ch1 + 32 == ch2 || ch1 == ch2 + 32)
                continue;
            else
                return false;
        }
        return true;
    }
    string GetAutoList(string content, int startPos, int len)
    {
        var Words = GetCacheWords(startPos, len);
        StringBuilder sb = new StringBuilder();
        foreach (var word in Words)
        {
            if (Match(word,content))
            {
                sb.Append(word);
                sb.Append(' ');
            }
        }
        if (sb.Length > 0)
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
    void TryAutoComplete()
    {
        // Find the word start
        var currentPos = this.scintilla1.CurrentPosition;
        var wordStartPos = this.scintilla1.WordStartPosition(currentPos, true);
        // Display the autocompletion list
        var lenEntered = currentPos - wordStartPos;
        string content = Text.Substring(wordStartPos, lenEntered);
        if (lenEntered > 0)
        {
            //this.scintilla1.AutoCShow(lenEntered, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            this.scintilla1.AutoCShow(lenEntered, GetAutoList(content, wordStartPos, lenEntered));
        }
    }
    private void CharDelete(object sender, ModificationEventArgs arg)
    {
        TryAutoComplete();
    }
    private void CharAdded(object sender, CharAddedEventArgs e)
    {
        TryAutoComplete();
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

    public override void OnDBClickNC()
    {
        DocumentManager.CreateDocument(string.Empty);
    }

    static void CloseAll()
    {
        foreach (var instance in mInstances)
        {
            DocumentManager.CloseDocument(instance.FileName);
            instance.Hide();
            instance.Dispose();
        }
        mInstances.Clear();
    }

    private void TextEditor_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        this.scintilla1.ClearRegisteredImages();
        mInstances.Remove(this);
        if (!string.IsNullOrEmpty(this.FileName))
            DocumentManager.CloseDocument(this.FileName);
    }

    private void closeAllToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        CloseAll();
    }
    protected override string GetPersistString()
    {
        return string.Format("Type={0};TabText={1}", GetType().Name, FileName);
    }
    public override void LoadFromPersistString(Core.PersistStringParser parser)
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
            DocumentManager.CreateDocument(file);
    }
}
