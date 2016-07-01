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


namespace TE2
{

[Core.ExtensionVersion(Name = "TE2")]

public partial class TE2 : Extension, ATrigger.ITriggerStatic
{
    public static Color GlobalBackColor = Color.FromArgb(0, 220, 230, 255);
    static List<TE2> mInstances = new List<TE2>();
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
            return this.textEditorControl1.Text;
        }
        set
        {
            this.textEditorControl1.Text = value;
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

    
    void Cut()
    {
    }
    public TE2()
    {
        InitializeComponent();

        this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
        ATrigger.DataCenter.AddInstance(this);
        mInstances.Add(this);

        this.textEditorControl1.SetHighlighting("C++");
    }

    static TE2 te2;

    [AddMenu("View(&V)/TE2")]
    static void OnOpenView()
    {
        if (te2 == null)
            te2 = new TE2();
        te2.Show(Center.Form.DockerContainer, DockState.Document);
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
    static ToolStripMenuItem mFileToolStripMenuItem;

    

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
        TE2 editor = (TE2)sender;
        if (editor.Visible)
            Center.ActiveDocument.value = string.IsNullOrEmpty(editor.FileName) ? editor.TabText : editor.FileName;
    }

    private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Center.ActiveDocument.value))
            Shell.OpenFloder(Center.ActiveDocument.value);
    }
}
}
