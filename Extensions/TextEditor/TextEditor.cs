using Core;
using ScintillaNET;
using Shortcut;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace TextEditor
{

[Core.ExtensionVersion(Name = "TextEditor")]

public partial class TextEditor : Extension, ATrigger.ITriggerStatic
{
    public static Color GlobalBackColor = Color.FromArgb(0, 220, 230, 255);
    static List<TextEditor> mInstances = new List<TextEditor>();
    static int CreateCount = 0;

    static string NewDocuemntName
    {
        get
        {
            ++CreateCount;
            return "New" + CreateCount.ToString();
        }
    }

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
            if (File.Exists(value))
            {
                mFileName = value;
                TabText = Path.GetFileName(value);
                this.Text = File.ReadAllText(mFileName);
            }
            else
            {
                TabText = value;
            }
        }
    }

    [ATrigger.Receiver((int)DataType.ActiveDocument)]
    static void OnActiveDocument()
    {
        foreach (var instance in mInstances)
        {
            string name = string.IsNullOrEmpty(instance.FileName) ? instance.TabText : instance.FileName;
            if (name == Center.ActiveDocument.value)
                instance.Show(Center.Form.DockerContainer, DockState.Document);
        }
    }

    [ATrigger.Receiver((int)DataType.ExtensionsLoaded)]
    public void OnExtensionLoaded()
    {
        throw new Exception();
    }
    void Cut()
    {
        this.scintilla1.Cut();
    }
    public TextEditor()
    {
        InitializeComponent();

        this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
        ATrigger.DataCenter.AddInstance(this);

        //Center.HotKey.Bind(Shortcut.Modifiers.Control, Keys.X).To(Cut);

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

        //Bitmap bmp = (Bitmap)Bitmap.FromFile("vol4.bmp");
        //this.scintilla1.RegisterRgbaImage(-1, bmp);

        mInstances.Add(this);
    }

    [AddMenu("View(&V)/TextEditor")]
    static void OnOpenView()
    {
        if (mInstances.Count == 0)
            Center.CurrentOpenDoucment.value = NewDocuemntName;
    }

    [ATrigger.Receiver((int)DataType.LayoutEnd)]
    static void OnLayoutEnd()
    {
        var histroy = new List<string>();
        foreach (var instance in mInstances)
        {
            string name = string.IsNullOrEmpty(instance.FileName) ? instance.TabText : instance.FileName;
            Center.DocumentManager.AddHistroy(name);
        }
        
        mFileMenuInitItemCount = FileToolStripMenuItem.DropDownItems.Count;

        LoadHistry(Center.Option.File.Histroy);
    }

    [ATrigger.Receiver((int)DataType.OpenDocument)]
    static public void CreateDocument()
    {
        TextEditor instance = new TextEditor();
        if(!File.Exists(Center.CurrentOpenDoucment.value))
            instance.TabText = Center.CurrentOpenDoucment.value;
        else
            instance.FileName = Center.CurrentOpenDoucment.value;
        instance.Show(Center.Form.DockerContainer, DockState.Document);
    }

    [ATrigger.Receiver((int)DataType.ChangeDocumentName)]
     public void OnNameChaned()
    {
        string oldname = Center.OnChangeDocumentName.Arg<string>(0);
        string newname = Center.OnChangeDocumentName.Arg<string>(1);

        if (oldname == this.TabText || oldname == this.FileName)
            this.FileName = newname;
    }

    static ToolStripMenuItem FileToolStripMenuItem
    {
        get
        {
            foreach (var item in Center.Form.MainMenu.Items)
            {
                ToolStripMenuItem menu = (ToolStripMenuItem)item;
                if (menu.Text == "File(&F)")
                {
                    mFileToolStripMenuItem = menu;
                    break;
                }
            }
            return mFileToolStripMenuItem;
        }
    }
    static int mFileMenuInitItemCount = 0;
    static ToolStripMenuItem mFileToolStripMenuItem;

    static void LoadHistry(List<string> histroy)
    {
        while (FileToolStripMenuItem.DropDownItems.Count > mFileMenuInitItemCount)
            FileToolStripMenuItem.DropDownItems.RemoveAt(FileToolStripMenuItem.DropDownItems.Count - 1);

        if (histroy.Count == 0)
            return;

        FileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
        FileToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem("Histroy"));

        for (int i = 0; i < histroy.Count; ++i)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = (i + 1).ToString() + " " + histroy[i];
            item.Click += OpenHistroyFile;
            FileToolStripMenuItem.DropDownItems.Add(item);
        }
    }

    [ATrigger.Receiver((int)DataType.OpenDocument)]
    static void OnDocumentOpen()
    {
        if (Center.Option.File.Histroy.Remove(Center.CurrentOpenDoucment.value))
            LoadHistry(Center.Option.File.Histroy);
    }

    [ATrigger.Receiver((int)DataType.CloseDocument)]
    static void OnDocumentClose()
    {
        while (Center.Option.File.Histroy.Count >= Center.Option.File.MaxHistroyCount)
            Center.Option.File.Histroy.RemoveAt(0);
        Center.Option.File.Histroy.Add(Center.CurrentCloseDoucment.value);
        LoadHistry(Center.Option.File.Histroy);
    }
    static void OpenHistroyFile(object sender, EventArgs e)
    {
        int pos = sender.ToString().IndexOf(' ');
        if (pos != -1)
            Center.CurrentOpenDoucment.value = sender.ToString().Substring(pos + 1);
        else
            throw new Exception();
    }
    static string GetTextFromInstances(string name)
    {
        foreach(var inst in mInstances)
        {
            if (inst.TabText == name || inst.FileName == name)
            {
                return inst.Text;
            }
        }
        return string.Empty;
    }


    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (string file in files)
            Center.CurrentOpenDoucment.value = file;
    }

    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
        {
            e.Effect = DragDropEffects.All;
        }
    }

    [AddShortKey(ShortCutIndex.SaveDocument, Modifiers.Control, Keys.S, "SaveFile")]
    static void SaveFile()
    {
        if (!string.IsNullOrEmpty(Center.ActiveDocument.value))
        {
            string text = GetTextFromInstances(Center.ActiveDocument.value);

            if (File.Exists(Center.ActiveDocument.value))
            {
                File.WriteAllText(Center.ActiveDocument.value, text);
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                var res = dlg.ShowDialog();

                if (res == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, text);

                    Center.OnChangeDocumentName.Trigger(Center.ActiveDocument, dlg.FileName);
                }
            }
        }
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
        char pair='\0';

        switch (e.Char)
        {
            case '{': pair = '}'; break;
            case '(': pair = ')'; break;
            case '[': pair = ']'; break;
        }

        if (pair != '\0')
        {
            this.scintilla1.InsertText(this.scintilla1.CurrentPosition, new string(pair, 1));
            return;
        }
        TryAutoComplete();
    }

    public override void OnDBClickNC()
    {
        Center.CurrentOpenDoucment.value = NewDocuemntName;
    }

    static void CloseAll()
    {
        foreach (var instance in mInstances)
        {
            Center.CurrentCloseDoucment.value = instance.FileName;
            instance.Hide();
            instance.Dispose();
        }
        mInstances.Clear();
    }

    private void TextEditor_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
    {
        ATrigger.DataCenter.RemoveInstance(this);

        this.scintilla1.ClearRegisteredImages();
        mInstances.Remove(this);
        if (!string.IsNullOrEmpty(this.FileName))
            Center.CurrentCloseDoucment.value = this.FileName;
    }

    private void closeAllToolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        CloseAll();
    }
    protected override string GetPersistString()
    {
        return string.Format("Type={0};TabText={1}", GetType().Name, FileName);
    }
    public override bool LoadFromPersistString(Core.PersistStringParser parser)
    {
        FileName = parser["TabText"];
        return !string.IsNullOrEmpty(FileName);
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
            Center.CurrentOpenDoucment.value = file;
    }

    private void TextEditor_VisibleChanged(object sender, EventArgs e)
    {
        TextEditor editor = (TextEditor)sender;
        if (editor.Visible)
            Center.ActiveDocument.value = string.IsNullOrEmpty(editor.FileName) ? editor.TabText : editor.FileName;
    }
}
}
